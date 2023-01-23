using System.Linq;
using Core.Damagables;
using UnityEngine;

namespace Misc.Damagables.Effects
{
    public class RagdollDirectionDamagableEffect : DamagablesEffect
    {
        public Vector3 RagdollPower { get; private set; }
        
        public override void TakeDamage(float damage, Team team, IDamageParameter[] damageParameters)
        {
            var parameter = damageParameters.FirstOrDefault(param => param is RepulsionParameter) as RepulsionParameter;
            
            if(parameter == null)
                return;

            RagdollPower = parameter.Direction * parameter.Power;
        }
    }
}