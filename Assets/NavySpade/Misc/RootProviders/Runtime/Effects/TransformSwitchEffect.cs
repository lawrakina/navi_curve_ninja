using System;
using UnityEngine;

namespace Misc.RootProviders.Runtime.Effects
{
    [Serializable]
    public abstract class TransformSwitchEffect : IDisposable
    {
        [field: SerializeField] public Vector3 InitialValue { get; protected set; }
        
        public abstract void Apply(Transform target, Action onComplete = null);

        public abstract void Dispose();
    }
}