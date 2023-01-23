using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;

namespace NavySpade.Modules.Utils
{
    /// <summary>
    /// Utility for calling functions on the main Unity thread from multithreaded code.
    /// </summary>
    public class Dispatcher : MonoBehaviour
    {
        private static Dispatcher _instance;

        private static Dispatcher Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = new GameObject("Dispatcher").AddComponent<Dispatcher>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        private Thread _mainThread;

        private ConcurrentQueue<(Action Action, ManualResetEventSlim Event)> _queue =
            new ConcurrentQueue<(Action Action, ManualResetEventSlim Event)>();

        private bool OnMainThread => Thread.CurrentThread == _mainThread;

        private void Awake()
        {
            _mainThread = Thread.CurrentThread;
        }

        private void Update()
        {
            while (_queue.Count > 0)
            {
                if (_queue.TryDequeue(out var item) == false)
                {
                    continue;
                }

                item.Action();
                item.Event?.Set();
            }
        }


        /// <summary>
        /// Runs some code on the main thread. If called from a non-main thread, will return immediately, before <paramref name="action"/> has finished running.
        /// </summary>
        public static void InvokeAsync(Action action)
        {
            if (Instance.OnMainThread)
            {
                action();
            }
            else
            {
                Instance._queue.Enqueue((action, null));
            }
        }

        /// <summary>
        /// Runs some code on the main thread. Will wait for <paramref name="action"/> to finish running before it returns.
        /// </summary>
        public static void Invoke(Action action)
        {
            if (Instance.OnMainThread)
            {
                action();
                return;
            }

            using (var ev = new ManualResetEventSlim())
            {
                Instance._queue.Enqueue((action, ev));
                ev.Wait();
            }
        }
    }
}