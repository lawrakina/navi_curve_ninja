using UnityEngine.UI;

namespace NavySpade.Modules.Extensions.UI
{
    public static class ScrollRectExtensions
    {
        public static void ScrollToBottom(this ScrollRect scrollRect)
        {
            scrollRect.verticalNormalizedPosition = 0;
        }

        public static void ScrollToTop(this ScrollRect scrollRect)
        {
            scrollRect.verticalNormalizedPosition = 1;
        }
    }
}