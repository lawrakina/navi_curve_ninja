using UnityEngine;

namespace Main.Levels.AdditionData
{
    public class Fog : ILevelExtensionData
    {
        public bool IsEnable;
        public Color Color;
        public float StartDistance;
        public float EndDistance;
        
        public void Apply()
        {
            RenderSettings.fog = IsEnable;
            RenderSettings.fogColor = Color;
            RenderSettings.fogStartDistance = StartDistance;
            RenderSettings.fogEndDistance = EndDistance;
        }

        public void Clear()
        {
        }
    }
}