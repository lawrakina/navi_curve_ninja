using System;
using Depra.Toolkit.RootProviders.Runtime.Routers;
using NavySpade.Modules.Extensions.UnityTypes;
using UnityEngine;

namespace Misc.RootProviders.Runtime.Routers
{
    [Serializable]
    [AddTypeMenu("Delay")]
    public class DelayedActionRouter : ExtendedMonoBehavior, IActionLifetimeRouter
    {
        [Min(0f)] [SerializeField] private float _delay = 0.2f;
        
        public void Init(Action onStarted, Action onCompleted)
        {
            onStarted?.Invoke();
            InvokeAtTime(_delay, onCompleted);
        }

        private void OnCompleted(Action onComplete)
        {
            StopAllCoroutines();
            onComplete?.Invoke();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}