using System;
using AYellowpaper;
using Depra.Toolkit.RootProviders.Runtime.Routers;
using Misc.RootProviders.Runtime.Base;
using NaughtyAttributes;
using UnityEngine;

namespace NavySpade.Misc.RootProviders.Runtime.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Animator")]
    public class AnimatorRoot : RootProvider
    {
        [Required] [SerializeField] private Animator _animator;
        [Required] [SerializeField] private InterfaceReference<IActionLifetimeRouter> _actionRouter;
        [SerializeField] private GameObject _subRoot;
        [AnimatorParam("_animator"), SerializeField] private int _showTriggerHash;
        [AnimatorParam("_animator"), SerializeField] private int _hideTriggerHash;

        public override bool IsActive => Root.activeInHierarchy;
        public override GameObject Root => _subRoot ? _subRoot : _animator.gameObject;

        protected override void ShowOverride(Action onComplete)
        {
            Root.SetActive(true);

            _actionRouter.Value.Init(
                () => { _animator.SetTrigger(_showTriggerHash); },
                onComplete);
        }

        protected override void HideOverride(Action onComplete)
        {
            _actionRouter.Value.Init(
                () => { _animator.SetTrigger(_hideTriggerHash); },
                onComplete);
        }

        public override void Reset(GameObject parent)
        {
            if (IsValid() == false)
            {
                _animator = parent.GetComponentInChildren<Animator>(true);
                _actionRouter.Value = parent.GetComponentInChildren<IActionLifetimeRouter>(true);
            }
        }

        public override bool IsValid()
        {
            return _animator && _actionRouter != null;
        }
    }
}