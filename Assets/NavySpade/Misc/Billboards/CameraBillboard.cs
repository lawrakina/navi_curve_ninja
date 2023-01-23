using UnityEngine;

namespace Misc.Billboards
{
    public class CameraBillboard : BillboardBase
    {
        public Vector3 rotationOffset = Vector3.zero;

        private void Awake()
        {
            Init(Camera.main.transform);
        }

        protected override void LookAtTarget()
        {
            transform.rotation = Target.rotation * Quaternion.identity * Quaternion.Euler(rotationOffset);
        }
    }
}