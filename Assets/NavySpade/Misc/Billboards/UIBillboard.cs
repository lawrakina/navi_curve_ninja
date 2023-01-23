using UnityEngine;

namespace Misc.Billboards
{
    [RequireComponent(typeof(Canvas))]
    public class UIBillboard : BillboardBase
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _yOffset = 1.5f;

        private Camera _camera;

        private void Awake()
        {
            Init(transform.parent);
            _camera = Camera.main;
            _canvas.worldCamera = _camera;

            transform.SetParent(_camera.transform);
        }

        private void OnBecameVisible()
        {
            enabled = true;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        protected override void LookAtTarget()
        {
            if (Target == null)
            {
                Destroy(gameObject);
                return;
            }
            
            var newPosition = new Vector3(Target.position.x, Target.position.y + _yOffset, Target.position.z);
            transform.position = newPosition;
        }
    }
}