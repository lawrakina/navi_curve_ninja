using System;
using DG.Tweening;
using Extensions.Structs;
using UnityEngine;

namespace Misc.RootProviders.Runtime.Effects
{
    [Serializable]
    [AddTypeMenu("DO Scale")]
    public class DoScaleTransformEffect : TransformSwitchEffect
    {
        [SerializeField] private Vector3 _delta = Vector3.one;
        [SerializeField] private bool _useLocal = true;
        [SerializeField] private ExtendedTweenParameters _tweenParameters = new ExtendedTweenParameters();

        private Tweener _animationTween;
        
        public override void Apply(Transform target, Action onComplete = null)
        {
            target.localScale = InitialValue;

            var initialScale = _useLocal ? target.localScale : InitialValue;
            var targetScale = initialScale + _delta;

            _animationTween.Kill();
            _animationTween = target.DOScale(targetScale, _tweenParameters.Duration)
                .SetUpdate(_tweenParameters.Update)
                .SetEase(_tweenParameters.Ease)
                .OnComplete(() => onComplete?.Invoke());
        }

        public override void Dispose()
        {
            _animationTween.Kill();
        }
    }
}