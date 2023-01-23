using System;
using Depra.Toolkit.RootProviders.Runtime.Base;
using Misc.RootProviders.Runtime.Base;
using UnityEngine;

namespace Depra.Toolkit.RootProviders.Runtime.Behaviors
{
    [Serializable]
    [AddTypeMenu("Root Behavior Router")]
    public class RootBehaviorRouter : IRootProvider
    {
        [SerializeField] private RootBehavior _behavior;

        public bool IsActive => _behavior.IsActive;

        public void Show(Action onComplete) => _behavior.Show(onComplete);

        public void Hide(Action onComplete) => _behavior.Hide(onComplete);

        public void HideInstantly() => _behavior.HideInstantly();
    }
}