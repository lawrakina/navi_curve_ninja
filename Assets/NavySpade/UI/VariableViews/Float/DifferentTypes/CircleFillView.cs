using UnityEngine.UI;

namespace Misc.VariableViews.Float.DifferentTypes
{
    public class CircleFillView : ImageFillView
    {
        protected override void Prepare()
        {
            FillingImage.type = Image.Type.Filled;
            FillingImage.fillMethod = Image.FillMethod.Radial360;
        }
    }
}