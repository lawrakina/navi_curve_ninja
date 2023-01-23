using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Damagables
{
    [System.Serializable]
    public class DamagebleEvents
    {
        public UnityEvent OnDead;
        public UnityEvent OnTakeDamage;
        public UnityEvent OnHeal;
    }
}
