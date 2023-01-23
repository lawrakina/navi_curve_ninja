using Core.Damagables;
using Misc.Damagables.Effects;
using UnityEngine;

namespace Misc.Damagables
{
    public class DamageCollider : MonoBehaviour
    {
        [SerializeField] private Team _attachedTeam = Team.Enemy;
        [SerializeField] private int _damageCount = 1;
        [SerializeField] private float _force = 1;

        
        //знаю что тупо, но без методов OnEnable и OnDisable галочка на включение/выключение объекта не появляется
        private bool _isEnable;

        private void OnEnable()
        {
            _isEnable = true;
        }

        private void OnDisable()
        {
            _isEnable = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if(_isEnable == false)
                return;
            
            if(other.gameObject.TryGetComponent<Damageble>(out var damagable) == false)
                return;
            
            ThrowDamage(damagable);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isEnable == false)
                return;
            
            if(other.gameObject.TryGetComponent<Damageble>(out var damagable) == false)
                return;
            
            ThrowDamage(damagable);
        }

        private void ThrowDamage(Damageble damageable)
        {
            var dir = damageable.transform.position - transform.position;
            dir.Normalize();
            
            damageable.DealDamage(_damageCount, _attachedTeam, new RepulsionParameter(dir, _force));
        }
    }
}