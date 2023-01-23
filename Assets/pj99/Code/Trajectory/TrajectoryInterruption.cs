using System;
using NaughtyAttributes;
using UnityEngine;

namespace Scripts.Shooting.Trajectory
{
    public class TrajectoryInterruption : MonoBehaviour
    {
        [SerializeField] private LineRenderer _mainLineRenderer;
        [SerializeField] private LineRenderer _reflectLineRenderer;
        [SerializeField] private float _reflectLineOffsetZ;
        [SerializeField] private GameObject _hitFxPrefab;
        [SerializeField, Layer] private int _shieldLayer;
        [SerializeField, Layer] private int _playerLayer;

        private GameObject _hitFx;
        private bool _wasHitShield;

        public Action OnStop;
        public Action OnHitShield;
        private int _shieldLinePosition;

        private void Awake()
        {
            _hitFx = Instantiate(_hitFxPrefab, transform);
            _hitFx.SetActive(false);
        }

        public void Reset()
        {
            _reflectLineRenderer.positionCount = 0;
            _wasHitShield = false;
        }

        public void Check(Vector3 position, GameObject collidedObject, int linePositionIndex)
        {
            if (!_wasHitShield && collidedObject.layer == _playerLayer)
            {
                Hit(position, collidedObject);
                return;
            }

            if (!_wasHitShield && collidedObject.layer == _shieldLayer)
            {
                _shieldLinePosition = linePositionIndex;
                _wasHitShield = true;
                return;
            }

            if (!_wasHitShield)
            {
                return;
            }

            Hit(position, collidedObject);
        }

        private void Hit(Vector3 position, GameObject collidedObject)
        {
            _hitFx.transform.position = position;
            OnStop?.Invoke();

            // if (collidedObject.TryGetComponent<SimulatedObstacle>(out var simulatedObstacle))
            // {
                // simulatedObstacle.PlayHit();
            // }
        }

        public void Cut()
        {
            if (!_wasHitShield)
            {
                return;
            }

            _reflectLineRenderer.positionCount = _mainLineRenderer.positionCount - _shieldLinePosition;
            var index = _shieldLinePosition;
            for (var i = 0; i < _reflectLineRenderer.positionCount; i++)
            {
                var position = _mainLineRenderer.GetPosition(index);
                position.z += _reflectLineOffsetZ;
                _reflectLineRenderer.SetPosition(i, position);
                index++;
            }
        }

        public void StopObserve(bool wasStopped)
        {
            _hitFx.SetActive(wasStopped);
        }
    }
}