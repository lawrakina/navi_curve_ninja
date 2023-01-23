using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.Modules.Utils.GradientColorize
{
    [AddComponentMenu("UI/Image Gradient Colorize")]
    [RequireComponent(typeof(Image))]
    public class GradientColorizeSprite : MonoBehaviour
    {
        public Image BackgroundImage;

        public Color LeftDownColor;
        public Color LeftUpColor;
        public Color RightDownColor;
        public Color RightUpColor;
    
        private Texture2D _texture2d = default;

        private void Start()
        {
            _texture2d = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            _texture2d.SetPixel(0,0,LeftDownColor);
            _texture2d.SetPixel(0,1,LeftUpColor);
            _texture2d.SetPixel(1,0,RightDownColor);
            _texture2d.SetPixel(1,1,RightUpColor);
            _texture2d.wrapMode = TextureWrapMode.Clamp;
            _texture2d.Apply();
            BackgroundImage.sprite =  Sprite.Create(_texture2d,new Rect(0,0,1,1), Vector2.zero);//_texture2d;
        
            //2m = 6s
            //1m = 3s
            //7m = 
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
