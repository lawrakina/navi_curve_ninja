using UnityEngine;

namespace Project20.PowerUps
{
    public interface IPositionCondition
    {
        bool IsMet(Vector3 position);
    }
}