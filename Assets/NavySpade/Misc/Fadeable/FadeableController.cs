using NavySpade.Modules.Extensions.UnityStructs;
using UnityEngine;

namespace Misc.Fadeable
{
    public class FadeableController : MonoBehaviour
    {
        [SerializeField] private LevelFadeableProvider _fadeablesProvider;
        [SerializeField] private Camera _camera;
        
        private Transform _target;

        private FadeableParameters Parameters => new FadeableParameters();

        private void Update()
        {
            if (_target == false || Parameters.Enabled == false)
            {
                return;
            }

            UpdateVisibility(_target.position);
        }

        public void UpdateVisibility(Vector3 targetPosition)
        {
            var distanceToTarget = GetDistanceTo(targetPosition);
            foreach (var obstacle in _fadeablesProvider.Components)
            {
                var distanceToHiding = GetDistanceTo(obstacle.transform.position);
                if (distanceToHiding < distanceToTarget)
                {
                    obstacle.FadeOut();
                }
                else
                {
                    obstacle.FadeIn();
                }
            }
        }

        public void Enable(Transform target)
        {
            _target = target;
        }

        public void Disable()
        {
            _target = null;
            ShowAll();
        }

        public void ShowAll()
        {
            foreach (var hiding in _fadeablesProvider.Components)
                hiding.FadeIn();
        }

        public void HideAll()
        {
            foreach (var hiding in _fadeablesProvider.Components)
                hiding.FadeOut();
        }

        private float GetDistanceTo(Vector3 target)
        {
            return _camera.transform.position.SqrDistance(target);
        }

        private void Init()
        {
            foreach (var hiding in _fadeablesProvider.Components)
            {
                hiding.SetParameters(Parameters);
            }
        }

        public void Reset()
        {
            _fadeablesProvider = GetComponentInChildren<LevelFadeableProvider>();
            Init();
        }
    }
}