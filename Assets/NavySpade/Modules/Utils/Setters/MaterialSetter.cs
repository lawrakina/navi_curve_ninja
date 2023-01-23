using UnityEngine;

namespace NavySpade.Modules.Utils.Setters
{
    public class MaterialSetter : MonoBehaviour
    {
        [SerializeField] private Material _material = default;
        [SerializeField] private Renderer[] _renderers = default;

        public void SetMaterial()
        {
            foreach (var renderer in _renderers)
            {
                renderer.material = _material;
            }
        }

        private void Reset()
        {
            _renderers = GetComponentsInChildren<Renderer>();
        }
    }
}
