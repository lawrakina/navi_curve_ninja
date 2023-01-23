using UnityEngine;

namespace Core.Damagables
{
    public abstract class DamagablesEffect : MonoBehaviour
    {
        public abstract void TakeDamage(float count, Team team, IDamageParameter[] damageParameters);
    }
}