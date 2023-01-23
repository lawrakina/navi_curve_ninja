using UnityEngine;

namespace NavySpade.Modules.Extensions.UI
{
    public static class GraphicExtensions
    {
        public static void SetAlpha(this UnityEngine.UI.Graphic graphic, float alpha)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
        }
    }
}