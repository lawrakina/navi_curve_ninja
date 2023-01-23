using System;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Events.Static;

namespace EventSystem.Runtime.Core.Handlers.Static
{
    public static class SingleEventHandler
    {
        private static SingleEvent _singleEvents;

        public static DisposeContainer Add(string key, Action action)
        {
            _singleEvents ??= new SingleEvent();
            _singleEvents.Add(key, action);

            return new DisposeContainer(() => Remove(key, action));
        }

        public static void Remove(string key, Action action)
        {
            _singleEvents?.Remove(key, action);
        }

        public static void Invoke(string key)
        {
            _singleEvents?.Invoke(key);
        }
    }
}