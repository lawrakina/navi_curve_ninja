using System;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Events.Dynamic;

namespace EventSystem.Runtime.Core.Handlers.Dynamic
{
    public static class DynamicArgsHandler
    {
        private static DynamicArgsEvent _events;

        public static DisposeContainer Add(string key, Action<dynamic[]> action)
        {
            _events ??= new DynamicArgsEvent();
            _events.Add(key, action);

            return new DisposeContainer(() => Remove(key, action));
        }

        public static void Remove(string key, Action<dynamic[]> action)
        {
            _events?.Remove(key, action);
        }

        public static void Invoke(string key, params dynamic[] value)
        {
            _events?.Invoke(key, value);
        }
    }
}