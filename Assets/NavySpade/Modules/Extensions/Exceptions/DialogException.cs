using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NavySpade.Modules.Extensions.Exceptions
{
    /// <summary>
    /// Critical exception class.
    /// </summary>
    public static class DialogException
    {
        /// <summary>
        /// Окно с критической ошибкой.
        /// появляется по верх всех окон юнити и стопорит процессы.
        /// </summary>
        /// <param name="message">Message</param>
        public static void ShowError(string message)
        {
#if UNITY_EDITOR
            var needExit = EditorUtility.DisplayDialog("ERROR!!!", message, "Exit", "Continue");
            if (needExit)
            {
                EditorApplication.isPlaying = false;
            }
            else
            {
                Debug.LogError(message);
            }
#else
            Debug.LogError(message);
#endif
        }
    }
}