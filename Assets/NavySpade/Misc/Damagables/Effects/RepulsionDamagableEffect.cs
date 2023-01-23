using System.Linq;
using Core.Damagables;
using UnityEngine;

namespace Misc.Damagables.Effects
{
    [RequireComponent(typeof(Rigidbody))]
    public class RepulsionDamagableEffect : DamagablesEffect
    {
        private Vector3 _currentPunchForce;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(_rb == null)
                return;
            
            var lenght = _currentPunchForce.magnitude;

            _currentPunchForce -= _currentPunchForce.normalized * (lenght * Time.fixedDeltaTime); 

            _rb.velocity = _currentPunchForce;
        }
        
        public override void TakeDamage(float damage, Team team, IDamageParameter[] damageParameters)
        {
            var parameter = damageParameters.FirstOrDefault(param => param is RepulsionParameter) as RepulsionParameter;
            
            if(parameter == null)
                return;
            
            AddPunchForce(parameter.Direction * parameter.Power);
        }
        
        public virtual void AddPunchForce(Vector3 power)
        {
            power.y = 0;
            _currentPunchForce += power;
        }
    }
}