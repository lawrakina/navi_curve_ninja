using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Misc.VariableViews.Float.DifferentTypes
{
    public class ImageFillView : DirectionalFloatView
    {
        [field: SerializeField] public Image FillingImage { get; private set; }

        protected virtual void Awake()
        {
            Assert.IsNotNull(FillingImage, "Filling image is NULL!");
            
            Prepare();
        }

        public override void Clear()
        {
            FillingImage.fillAmount = 0f;
            SetValue(0f);
        }
        
        protected override void InternalSetValue(float value)
        {
            FillingImage.fillAmount = value;
        }
        
        protected override void OnValueChanged(float value)
        {
            if (Mathf.Approximately(value, 1f) == false)
                return;

            Disable();
        }

        public void Reset()
        {
            FillingImage = GetComponentInChildren<Image>();
        }

        protected virtual void Prepare()
        {
            FillingImage.type = Image.Type.Filled;
        }
    }
}