using System;
using System.Collections;
using System.Text;
using Core.Game;
using NavySpade.Core.Runtime.Game;
using UnityEngine;

namespace Core.Editor.DeveloperTools
{
    public static class ScreenShooter
    {
        private const string TagToIgnore = "IgnoreForScreenshot";

        public static void TakeScreenshot(bool exceptions = true)
        {
            var name = GetScreenshotName();

            if (exceptions == false && Application.isPlaying)
            {
                GameLogic.Instance.StartCoroutine(CaptureScreen(name));
            }
            else
            {
                ScreenCapture.CaptureScreenshot(name);
            }

            Debug.LogWarning($"Screen capture: {GetScreenshotPath()}/{name}");
        }

        private static IEnumerator CaptureScreen(string name)
        {
            var exceptions = GameObject.FindGameObjectsWithTag(TagToIgnore);
            foreach (var exception in exceptions)
            {
                exception.SetActive(false);
            }

            yield return new WaitForEndOfFrame();

            ScreenCapture.CaptureScreenshot(name);

            foreach (var exception in exceptions)
            {
                exception.SetActive(true);
            }
        }

        private static string GetScreenshotName()
        {
            var dateTime = DateTime.Now;

            var stringBuilder = new StringBuilder("Screen")
                .Append(dateTime.Year.ToString("0000"))
                .Append(dateTime.Year.ToString("0000"))
                .Append('_')
                .Append(dateTime.Month.ToString("00"))
                .Append('_')
                .Append(dateTime.Day.ToString("00"))
                .Append('_')
                .Append(dateTime.Hour.ToString("00"))
                .Append('_')
                .Append(dateTime.Minute.ToString("00"))
                .Append('_')
                .Append(dateTime.Second.ToString("00"))
                .Append('_')
                .Append(dateTime.Millisecond.ToString("0000"))
                .Append(".png");

            return stringBuilder.ToString();
        }

        private static string GetScreenshotPath()
        {
            string dataPath;
#if UNITY_ANDROID || UNITY_IOS
            dataPath = Application.persistentDataPath;
#else
            dataPath = Application.dataPath;
#endif

            return dataPath;
        }
    }
}