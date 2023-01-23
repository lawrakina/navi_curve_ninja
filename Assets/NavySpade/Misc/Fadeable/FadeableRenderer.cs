using DG.Tweening;
using UnityEngine;

namespace Misc.Fadeable
{
    public class FadeableRenderer : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer = default;

        [HideInInspector] public FadeableParameters parameters;

        public bool IsHided { get; private set; }

        private Tweener _fadeTween;

        public void SetParameters(FadeableParameters parameters)
        {
            this.parameters = parameters;
        }

        public void FadeIn()
        {
            if (IsHided == false)
                return;

            _fadeTween.Kill();
            _fadeTween = _renderer.material.DOFade(1f, parameters.FadeInDuration)
                .OnComplete(() => ResetMaterialAlpha(_renderer.material));

            IsHided = false;
        }

        public void FadeOut()
        {
            if (IsHided)
                return;

            _fadeTween.Kill();
            _fadeTween = _renderer.material.DOFade(parameters.Alpha, parameters.FadeOutDuration);

            IsHided = true;
        }

        private void ResetMaterialAlpha(Material material)
        {
            material.color = new Color(
                material.color.r,
                material.color.g,
                material.color.b,
                1f);
        }

        private void Reset()
        {
            _renderer = GetComponentInChildren<Renderer>();
        }
    }
}