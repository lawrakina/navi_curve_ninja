using System;
using System.Collections;
using NavySpade.Modules.Utils.Singletons.Runtime.Unity;
using UnityEngine;

namespace NavySpade.Modules.Utils.Coroutines
{
    /// <summary>
    /// Run coroutines from static methods or from disabled <see cref="GameObject"/>.
    /// </summary>
    public class CoroutineUtility : MonoSingleton<CoroutineUtility>
    {
        public static Coroutine Run(IEnumerator enumerator)
        {
            Coroutine coroutine = null;
            Dispatcher.Invoke(() => { coroutine = Instance.StartCoroutine(enumerator); });

            return coroutine;
        }

        public static void StopRunning(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                Dispatcher.Invoke(() => { Instance.StopCoroutine(coroutine); });
            }
        }

        public static Coroutine RunAfterOneFrame(Action action)
        {
            return RunAfterFrameDelay(action, 1);
        }

        public static Coroutine RunAfterFrameDelay(Action action, int framesToWait)
        {
            var coroutine = Run(Routine());

            IEnumerator Routine()
            {
                for (var i = 0; i < framesToWait; i++)
                {
                    yield return new WaitForEndOfFrame();
                }

                action.Invoke();
            }

            return coroutine;
        }

        public static Coroutine RunAfterSecondsDelay(Action action, float secondsToWait)
        {
            var coroutine = Run(Routine());

            IEnumerator Routine()
            {
                yield return new WaitForSeconds(secondsToWait);
                action.Invoke();
            }

            return coroutine;
        }

        public static void RunAfterDelay(Action action, TimeSpan delay)
        {
            RunAfterSecondsDelay(action, (float)delay.TotalSeconds);
        }
    }
}