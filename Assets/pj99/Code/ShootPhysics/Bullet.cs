using System;
using Core.Damagables;
using Misc.Damagables;
using NaughtyAttributes;
using UnityEngine;

namespace Scripts.Shooting
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private DamageCollider _damageCollider;
        // [SerializeField] private Material _playerTeamMaterial;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Transform _visual;
        [SerializeField] private DestroyDetector _destroyDetector;
        // [SerializeField, Range(0, 1)] private float _bounciness;
        // [SerializeField, Layer] private int _shieldLayer;
        // [SerializeField, Layer] private int _simulationAfterShieldLayer;
        
        private int _defaultLayer;

        private int _ricochetsCount;
        private int _collisionsCount;
        private PhysicMaterial _physicMaterial;
        private bool _isSimulation;
        public Vector3 RigidBodyPosition => _rigidbody.position;

        public Action<Vector3, GameObject> OnCollided;
        public Action OnDestroyed;

        private void Awake()
        {
            _defaultLayer = gameObject.layer;
            
            _destroyDetector.OnDestroy += Destroy;
        }

        private void OnCollisionEnter(Collision collision)
        {
            _collisionsCount++;

            var position = collision.contacts[0].point + collision.contacts[0].normal * _collider.radius;
            OnCollided?.Invoke(position, collision.collider.gameObject);

            if (_isSimulation){
                {
                    // position.z = 0.2f;
                    // OnCollided?.Invoke(position, collision.collider.gameObject);
                    // if (collision.gameObject.layer == _shieldLayer)
                    // {
                    // gameObject.layer = _simulationAfterShieldLayer;
                    // }
                }
            }
            else
            {
                // if (collision.gameObject.layer == _shieldLayer)
                // {
                //     _damageCollider.ChangeTeam(Team.Player);
                //     var materials = _renderer.materials;
                //     materials[0] = _playerTeamMaterial;
                //     _renderer.materials = materials;
                //     
                //     FXEmitter.ThrowHitShield(position);
                // }
                // else
                // {
                //     FXEmitter.ThrowHitWall(position);
                // }
            }

            if (_collisionsCount < _ricochetsCount)
            {
                return;
            }

            if (_isSimulation)
            {
                Stop();
            }
            else
            {
                Destroy();
            }
        }

        public void Init(int ricochetsCount, bool isSimulation = false)
        {
            _ricochetsCount = ricochetsCount;
            _isSimulation = isSimulation;

            _physicMaterial = new PhysicMaterial();
            // _physicMaterial.bounciness = _bounciness;
            _physicMaterial.dynamicFriction = 0f;
            _physicMaterial.staticFriction = 0f;
            _physicMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
            _physicMaterial.bounceCombine = PhysicMaterialCombine.Maximum;

            _collider.material = _physicMaterial;

            _renderer.enabled = !isSimulation;
        }

        private void Update()
        {
            if (_rigidbody.velocity == Vector3.zero)
            {
                return;
            }

            _visual.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }

        private void Destroy()
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }

        public void Stop()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.inertiaTensor = Vector3.zero;
        }

        public void Reset(Vector3 position, Quaternion rotation)
        {
            gameObject.layer = _defaultLayer;
            _collisionsCount = 0;
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Shoot(Vector3 force)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}