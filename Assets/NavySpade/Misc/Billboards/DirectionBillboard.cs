using UnityEngine;

namespace Misc.Billboards
{
    public class DirectionBillboard : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction;

        private void LateUpdate()
        {
            RotateToDirection();
        }

        private void RotateToDirection()
        {
            transform.eulerAngles = 90f * _direction;
        }
    }
}