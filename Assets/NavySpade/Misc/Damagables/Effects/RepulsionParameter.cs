using Core.Damagables;
using UnityEngine;

namespace Misc.Damagables.Effects
{
    public class RepulsionParameter : IDamageParameter
    {
        public Vector3 Direction { get; }
        public float Power { get; }

        public RepulsionParameter(Vector3 direction, float power)
        {
            Direction = direction;
            Power = power;
        }
    }
}