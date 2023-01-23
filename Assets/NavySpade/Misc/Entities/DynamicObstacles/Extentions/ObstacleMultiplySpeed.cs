using UnityEngine;

namespace Misc.Entities.DynamicObstacles.Extentions
{
    [RequireComponent(typeof(DynamicObstacle))]
    public class ObstacleMultiplySpeed : MonoBehaviour
    {
        public AnimationCurve _speedMultiply;
        
        private DynamicObstacle _dynamicObstacle;

        private float _startSpeed;
        private float _time;

        private void Awake()
        {
            _dynamicObstacle = GetComponent<DynamicObstacle>();
        }

        private void Start()
        {
            _startSpeed = _dynamicObstacle.Speed;
        }

        private void OnDisable()
        {
            _dynamicObstacle.Speed = _startSpeed;
        }

        private void FixedUpdate()
        {
            _dynamicObstacle.Speed = _startSpeed * _speedMultiply.Evaluate(_time);

            _time += Time.fixedDeltaTime;
        }
    }
}