using UnityEngine;

namespace Main.Levels.AdditionData
{
    public class Skybox : ILevelExtensionData
    {
        public Material Material;
        
        public void Apply()
        {
            if (QualitySettings.renderPipeline == null)
            {
                RenderSettings.skybox = Material;
            }
            else
            {
                var mainCamera = Camera.main;
                var skyboxComponent = mainCamera.GetComponent<UnityEngine.Skybox>(); 
                
                skyboxComponent.material = Material;
            }
        }

        public void Clear()
        {
            
        }
    }
}