using System;
using Depra.Toolkit.RootProviders.Runtime.Base;
using Misc.RootProviders.Runtime.Base;
using Misc.RootProviders.Runtime.Effects;
using UnityEngine;

namespace Depra.Toolkit.RootProviders.Runtime.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Transform")]
    public class TransformRoot : RootProvider
    {
        [SerializeField] private Transform _root;
        [SerializeReference, SubclassSelector] private TransformSwitchEffect _activationEffect;
        [SerializeReference, SubclassSelector] private TransformSwitchEffect _deactivationEffect;

        public override bool IsActive => Root.activeSelf;
        public override GameObject Root => _root.gameObject;

        protected override void ShowOverride(Action onComplete)
        {
            Root.SetActive(true);

            if (_activationEffect == null)
            {
                onComplete?.Invoke();
                return;
            }

            _activationEffect.Apply(_root, onComplete);
        }

        protected override void HideOverride(Action onComplete)
        {
            if (_deactivationEffect == null)
            {
                onComplete?.Invoke();
                return;
            }

            _deactivationEffect.Apply(_root, onComplete);
        }

        public override void Reset(GameObject parent)
        {
            if (IsValid() == false)
            {
                _root = parent.transform;
            }
        }

        public override bool IsValid()
        {
            return _root;
        }
    }
}