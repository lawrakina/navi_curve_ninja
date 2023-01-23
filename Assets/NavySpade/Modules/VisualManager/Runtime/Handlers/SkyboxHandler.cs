using NavySpade.Modules.Visual.Runtime.Data;
using UnityEngine;

namespace NavySpade.Modules.Visual.Runtime.Handlers
{
    public class SkyboxHandler : MonoBehaviour
    {
        [SerializeField] private Skybox _skybox;

        private Material _material;

        public void Init(SkyParameters parameters)
        {
            if (parameters.Enabled == false)
            {
                return;
            }

            if (parameters.Material)
            {
                SetMaterial(parameters.Material);
            }
            else if (_skybox.material)
            {
                _material = _skybox.material;
            }
        }

        private void SetMaterial(Material material)
        {
            if (material == null)
            {
                return;
            }

            _material = Instantiate(material);
            _skybox.material = _material;
        }
    }
}