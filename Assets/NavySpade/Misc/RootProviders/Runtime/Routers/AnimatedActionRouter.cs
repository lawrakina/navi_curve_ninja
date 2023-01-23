using System;
using UnityEngine;

namespace Depra.Toolkit.RootProviders.Runtime.Routers
{
    public class AnimatedActionRouter : MonoBehaviour, IActionLifetimeRouter
    {
        [SerializeField] private bool _callOnStarted = true;

        private event Action _started;
        private event Action _completed;

        public void Init(Action onStarted, Action onCompleted)
        {
            _started = onStarted;
            _completed = onCompleted;

            if (_callOnStarted)
            {
                OnStarted();
            }
        }

        public void OnStarted()
        {
            _started?.Invoke();
            _started = null;
        }

        public void OnCompleted()
        {
            _completed?.Invoke();
            _completed = null;
        }

        private void OnDestroy()
        {
            _started = null;
            _completed = null;
        }
    }
}