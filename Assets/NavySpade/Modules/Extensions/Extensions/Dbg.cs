using System;


namespace Core.Extensions{
    public static class Dbg{
        public static void Log(string message){
#if UNITY_EDITOR
            UnityEngine.Debug.Log($"{message}");
#endif
        }

        public static void Log(Enum @enum){
#if UNITY_EDITOR
            UnityEngine.Debug.Log($"Console.{@enum.GetType()}:{@enum}");
#endif
        }

        public static void Warning(string message){
#if UNITY_EDITOR
            UnityEngine.Debug.LogWarning($"{message}");
#endif
        }

        public static void Error(string message){
#if UNITY_EDITOR
            UnityEngine.Debug.LogError($"{message}");
#endif
        }
    }
}