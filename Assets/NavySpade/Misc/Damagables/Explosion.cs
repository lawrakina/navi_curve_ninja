using Core.Damagables;
using Misc.Damagables;
using UnityEngine;

namespace Misc.Entities
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _explosionRadius = 4;
        [SerializeField] private float _explosionForce = 100;
        [SerializeField] private float _explosionDamage = 5;
        [SerializeField] private LayerMask _explosionLayers;
        [SerializeField] private Team _attackTeam;

        [Header("Effects")]
        [SerializeField] private bool _destroyAfterExplosion = true;
        [SerializeField] private GameObject _explosionEffect;
        [SerializeField] private float _explosionEffectDuraction = 2;

        /// <summary>
        /// Взрыв с нанесением урона и откидыванием RigidBody
        /// </summary>
        public void Boom()
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _explosionRadius, _explosionLayers);

            foreach (Collider collider in overlappedColliders)
            {
                Rigidbody rb = collider.attachedRigidbody;
                IDamageble destroyable = collider.GetComponent<IDamageble>();

                if (destroyable != null)
                {
                    destroyable.DealDamage(_explosionDamage);
                }

                if (rb)
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
                }
            }

            if (_explosionEffect)
            {
                GameObject effect = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                effect.SetActive(true);
                Destroy(effect, _explosionEffectDuraction);
            }

            if (_destroyAfterExplosion)
            {
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _explosionRadius);
        }
    }
}