using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.Modules.Utils.GradientColorize
{
    /// <summary>
    /// 4х-цветный градиент
    /// </summary>
    [AddComponentMenu("UI/Raw Gradient Colorize")]
    [RequireComponent(typeof(RawImage))]
    public class GradientColorize : MonoBehaviour
    {
        public RawImage BackgroundRawImage;

        public Color LeftDownColor;
        public Color LeftUpColor;
        public Color RightDownColor;
        public Color RightUpColor;
    
        private Texture2D _texture2d;

        private void Start()
        {
            _texture2d = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            _texture2d.SetPixel(0,0,LeftDownColor);
            _texture2d.SetPixel(0,1,LeftUpColor);
            _texture2d.SetPixel(1,0,RightDownColor);
            _texture2d.SetPixel(1,1,RightUpColor);
            _texture2d.wrapMode = TextureWrapMode.Clamp;
            _texture2d.Apply();
            BackgroundRawImage.texture = _texture2d;
        }

        private void Update()
        {
#if UNITY_EDITOR
            _texture2d.SetPixel(0,0,LeftDownColor);
            _texture2d.SetPixel(0,1,LeftUpColor);
            _texture2d.SetPixel(1,0,RightDownColor);
            _texture2d.SetPixel(1,1,RightUpColor);
            _texture2d.Apply();
#endif
        }
    }
}
