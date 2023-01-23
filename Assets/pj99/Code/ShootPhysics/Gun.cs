using NaughtyAttributes;
using Scripts.Shooting.Trajectory;
using UnityEngine;

namespace Scripts.Shooting
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private TrajectoryDrawer _trajectoryDrawer;
        [field: SerializeField]
        public float Speed{ get; set; }
        [SerializeField] private int _ricochetsCount = 8;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Animator _visualAnimator;

        private bool _isAiming;
        private static readonly int Shoot = Animator.StringToHash("Shoot");

        [Button("Init")]
        public void Init()
        {
            _trajectoryDrawer.Init();
            _trajectoryDrawer.StartSimulate(_bulletPrefab, _shootPoint, GetShootPosition(), _ricochetsCount);
            _isAiming = true;
        }

        private void Update()
        {
            if (!_isAiming)
            {
                return;
            }

            _trajectoryDrawer.Simulate(GetForce(), GetShootPosition());
        }

        [Button("Push")]
        public Bullet Push()
        {
            // _visualAnimator.SetTrigger(Shoot);
            _isAiming = false;
            _trajectoryDrawer.StopSimulate();
            var bullet = Instantiate(_bulletPrefab, GetShootPosition(), _shootPoint.rotation);
            bullet.Init(_ricochetsCount);
            bullet.Shoot(GetForce());
            return bullet;
        }

        private Vector3 GetForce()
        {
            var force = _shootPoint.forward * Speed;
            force.z = 0;
            return force;
        }

        private Vector3 GetShootPosition()
        {
            var position = _shootPoint.position;
            position.z = 0.2f;
            return position;
        }

        public void Fall()
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
            transform.SetParent(null);
        }
    }
}