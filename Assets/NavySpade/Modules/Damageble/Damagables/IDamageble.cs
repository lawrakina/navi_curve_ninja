using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Damagables
{
    public interface IDamageble
    {
        public void DealDamage(float damage, Team team, params IDamageParameter[] damageParameters);
        public void DealDamage(float damage);
    }
}
