using System;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Handlers.Static;

namespace EventSystem.Runtime.Core.Managers
{
    public static class EventManager
    {
        #region Add events

        /// <summary>
        /// Subscribe event.
        /// Single event, dont take arguments.
        /// </summary>
        /// <param name="key">String key</param>
        /// <param name="action">Event action</param>
        /// <returns>Return dispose container</returns>
        public static DisposeContainer Add(string key, Action action) => SingleEventHandler.Add(key, action);
        
        /// <summary>
        /// subscribe event
        /// single event, dont take arguments
        /// </summary>
        /// <param name="key">enum key</param>
        /// <param name="action">event action</param>
        /// <returns>return dispose container</returns>
        public static DisposeContainer Add(Enum key, Action action) => SingleEventHandler.Add(key.ToString(), action);

        /// <summary>
        /// Subscribe event.
        /// Func event, take T Argument.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DisposeContainer Add<T>(string key, Func<T> action) => FuncEventHandler.Add(key, action);

        /// <summary>
        /// Subscribe event.
        /// Generic event, take T Argument.
        /// </summary>
        /// <param name="key">String key</param>
        /// <param name="action">Event action</param>
        /// <typeparam name="T">Argument type</typeparam>
        /// <returns>Return dispose container</returns>
        public static DisposeContainer Add<T>(string key, Action<T> action) => GenericEventHandler.Add(key, action);
        
        /// <summary>
        /// subscribe event
        /// Generic event, take T Argument
        /// </summary>
        /// <param name="key">enum key</param>
        /// <param name="action">event action</param>
        /// <typeparam name="T">argument type</typeparam>
        /// <returns>return dispose container</returns>
        public static DisposeContainer Add<T>(Enum key, Action<T> action) => GenericEventHandler.Add(key.ToString(), action);

        /// <summary>
        /// Subscribe event.
        /// Take an unspecified amount arguments.
        /// </summary>
        /// <param name="key">String key</param>
        /// <param name="action">Event action</param>
        /// <returns>Return dispose container</returns>
        public static DisposeContainer Add(string key, Action<object[]> action) => ObjectArgsHandler.Add(key, action);

        #endregion

        #region Remove events

        /// <summary>
        /// Remove single event.
        /// </summary>
        /// <param name="key">String key</param>
        /// <param name="action">Event action</param>
        public static void Remove(string key, Action action) => SingleEventHandler.Remove(key, action);
        
        /// <summary>
        /// remove single event
        /// </summary>
        /// <param name="key">Enum key</param>
        /// <param name="action">event action</param>
        public static void Remove(Enum key, Action action) => SingleEventHandler.Remove(key.ToString(), action);

        /// <summary>
        /// Remove function event.
        /// </summary>
        /// <param name="key">String key</param>
        /// <param name="action">Event action</param>
        /// <typeparam name="T">Argument type</typeparam>
        public static void Remove<T>(string key, Func<T> action) => FuncEventHandler.Remove(key, action);
        
        /// <summary>
        /// Remove generic event.
        /// </summary>
        /// <param name="key">String key</param>
        /// <param name="action">Event action</param>
        /// <typeparam name="T">Argument type</typeparam>
        public static void Remove<T>(string key, Action<T> action) => GenericEventHandler.Remove(key, action);

        /// <summary>
        /// remove generic event
        /// </summary>
        /// <param name="key">Enum key</param>
        /// <param name="action">event action</param>
        /// <typeparam name="T">argument type</typeparam>
        public static void Remove<T>(Enum key, Action<T> action) => GenericEventHandler.Remove(key.ToString(), action);
        
        /// <summary>
        /// Remove unspecified amount event.
        /// </summary>
        /// <param name="key">String key</param>
        /// <param name="action">Event action</param>
        public static void Remove(string key, Action<object[]> action) => ObjectArgsHandler.Remove(key, action);

        /// <summary>
        /// remove unspecified amount event
        /// </summary>
        /// <param name="key">Enum key</param>
        /// <param name="action">event action</param>
        public static void Remove(Enum key, Action<object[]> action) => ObjectArgsHandler.Remove(key.ToString(), action);
        
        #endregion

        #region Invoke events

        /// <summary>
        /// Invoke single event.
        /// </summary>
        /// <param name="key">String event key</param>
        public static void Invoke(string key) => SingleEventHandler.Invoke(key);

        /// <summary>
        /// invoke single event
        /// </summary>
        /// <param name="key">Enum event key</param>
        public static void Invoke(Enum key) => SingleEventHandler.Invoke(key.ToString());
        
        /// <summary>
        /// Invoke func event.
        /// </summary>
        /// <param name="key">String event key</param>
        /// <typeparam name="T">Argument type</typeparam>
        public static void Invoke<T>(string key) => FuncEventHandler.Invoke<T>(key);
        
        /// <summary>
        /// invoke generic event
        /// </summary>
        /// <param name="key">Enum event key</param>
        /// <param name="value">T argument variable</param>
        /// <typeparam name="T">argument type</typeparam>
        public static void Invoke<T>(Enum key, T value) => GenericEventHandler.Invoke(key.ToString(), value);
        
        /// <summary>
        /// Invoke generic event.
        /// </summary>
        /// <param name="key">String event key</param>
        /// <param name="value">T argument variable</param>
        /// <typeparam name="T">Argument type</typeparam>
        public static void Invoke<T>(string key, T value) => GenericEventHandler.Invoke(key, value);
        
        /// <summary>
        /// Invoke unspecified amount event.
        /// </summary>
        /// <param name="key">String event key</param>
        /// <param name="value">Unspecified amount value</param>
        public static void InvokeArray(string key, params object[] value) => ObjectArgsHandler.Invoke(key, value);

        /// <summary>
        /// invoke unspecified amount event
        /// </summary>
        /// <param name="key">Enum event key</param>
        /// <param name="value">unspecified amount value</param>
        public static void Invoke(Enum key,params object[] value) => ObjectArgsHandler.Invoke(key.ToString(), value);
        
        #endregion
    }
}