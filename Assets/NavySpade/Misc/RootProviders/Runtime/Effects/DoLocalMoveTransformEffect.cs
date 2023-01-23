using System;
using DG.Tweening;
using Extensions.Structs;
using UnityEngine;

namespace Misc.RootProviders.Runtime.Effects
{
    [Serializable]
    [AddTypeMenu("DOLocalMove")]
    public class DoLocalMoveTransformEffect : TransformSwitchEffect
    {
        [SerializeField] private Vector3 _delta = new Vector3(0f, 0.5f, 0f);
        [SerializeField] private bool _useLocal = true;
        [SerializeField] private ExtendedTweenParameters _tweenParameters = new ExtendedTweenParameters();

        private Tweener _animationTween;
        
        public override void Apply(Transform target, Action onComplete = null)
        {
            var startPosition = _useLocal ? target.localPosition : InitialValue;
            var targetPosition = startPosition + _delta;
            
            _animationTween.Kill();
            _animationTween = target.DOLocalMove(targetPosition, _tweenParameters.Duration)
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