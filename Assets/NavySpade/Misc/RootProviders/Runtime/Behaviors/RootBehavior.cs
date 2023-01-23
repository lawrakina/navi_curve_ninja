using System;
using Depra.Toolkit.RootProviders.Runtime.Base;
using Depra.Toolkit.RootProviders.Runtime.DifferentTypes;
using Misc.RootProviders.Runtime.Base;
using NaughtyAttributes;
using UnityEngine;

namespace Depra.Toolkit.RootProviders.Runtime.Behaviors
{
    public class RootBehavior : MonoBehaviour, IRootProvider
    {
        [SerializeReference, SubclassSelector] private RootProvider _root = new TransformRoot();

        public bool IsActive => _root.IsActive;

        private void OnEnable()
        {
            if (_root.IsValid() == false)
            {
                _root.Reset(gameObject);
            }

            Show();
        }

        [Button]
        public void Show() => Show(null);

        public void Show(Action onComplete) => _root.TryShow(onComplete);

        [Button]
        public void Hide() => _root.TryHide();

        public void Hide(Action onComplete) => _root.TryHide(onComplete);

        public void ShowInstantly() => _root.ShowInstantly();

        public void HideInstantly() => _root.HideInstantly();

        public void ChangeType(RootProvider newRoot)
        {
            _root = newRoot ?? throw new NullReferenceException("Root is null!");
        }

        [Button]
        public void Reset()
        {
            _root.Reset(gameObject);
        }
    }
}