using System;
using DG.Tweening;
using UnityEngine;

namespace Misc.VariableViews.Float.DifferentTypes
{
    public class TweenImageFillView : ImageFillView
    {
        [Min(0f)] [SerializeField] private float _duration = 0.3f;
        [SerializeField] private Ease _ease;

        private Tweener _fillingTween;

        public override void SetValue(float value)
        {
            SetValue(value, _duration);
        }

        public void SetValue(float value, Action onComplete)
        {
            SetValue(value, _duration, onComplete);
        }

        public void SetValue(float value, float duration, Action onComplete = null)
        {
            _fillingTween.Kill();
            
            _fillingTween = FillingImage.DOFillAmount(value, duration)
                .SetEase(_ease)
                .OnComplete(() =>
                {
                    OnValueChanged(value);
                    onComplete?.Invoke();
                });
        }
        
        public void SetValueInstantly(float value)
        {
            FillingImage.fillAmount = value;
            OnValueChanged(value);
        }
        
        protected virtual void OnDestroy()
        {
            _fillingTween.Kill();
        }
    }
}