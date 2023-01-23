using UnityEngine;
using UnityEngine.Assertions;

namespace NavySpade.Modules.Selection.Runtime.RayProviders
{
    public class MouseScreenRayProvider : MonoBehaviour, IRayProvider
    {
        [SerializeField] private Camera _camera;

        private void Start()
        {
            Assert.IsNotNull(_camera);
        }

        public Ray CreateRay()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            return ray;
        }

        public void Reset()
        {
            _camera = FindObjectOfType<Camera>();
        }

        private void OnDrawGizmos()
        {
            if (_camera == null)
            {
                return;
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_camera.ScreenPointToRay(Input.mousePosition));
        }
    }
}