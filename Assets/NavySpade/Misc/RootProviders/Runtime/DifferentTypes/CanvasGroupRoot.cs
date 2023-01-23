using System;
using Depra.Toolkit.RootProviders.Runtime.Base;
using DG.Tweening;
using NaughtyAttributes;
using NavySpade.Modules.Extensions;
using UnityEngine;

namespace Misc.RootProviders.Runtime.DifferentTypes
{
    [Serializable]
    [AddTypeMenu("Canvas Group")]
    public class CanvasGroupRoot : UIRootProvider
    {
        [Required] [SerializeField] private CanvasGroup _canvasGroup;
        [MinMaxSlider(0f, 1f), SerializeField] private Vector2 _minMaxValues = new Vector2(0f, 1f);
        [Range(0f, 1f), SerializeField] private float _animationDuration = 0.2f;

        public override bool IsActive => Root.activeSelf && _canvasGroup.alpha.Approximately(MaxValue);
        public override GameObject Root => _canvasGroup.gameObject;

        private float MinValue => _minMaxValues.x;
        private float MaxValue => _minMaxValues.y;

        protected override void ShowOverride(Action onComplete)
        {
            _canvasGroup.alpha = MinValue;
            _canvasGroup.gameObject.SetActive(true);

            _canvasGroup.DOFade(MaxValue, _animationDuration).OnComplete(() => onComplete?.Invoke());
        }

        protected override void HideOverride(Action onComplete)
        {
            _canvasGroup.DOFade(MinValue, _animationDuration).OnComplete(() => onComplete?.Invoke());
        }

        protected override void OnEnabled(Action onComplete = null)
        {
            _canvasGroup.alpha = MaxValue;
            onComplete?.Invoke();
        }

        protected override void OnDisabled(Action onComplete = null)
        {
            _canvasGroup.alpha = MinValue;
            onComplete?.Invoke();
        }

        public override void Reset(GameObject parent)
        {
            if (IsValid() == false)
            {
                _canvasGroup = parent.GetComponentInChildren<CanvasGroup>();
            }
        }

        public override bool IsValid()
        {
            return _canvasGroup;
        }
    }
}