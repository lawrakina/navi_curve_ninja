using UnityEngine.UI;

namespace Toolkit.Extensions.UI
{
    public static class ScrollbarExtensions
    {
        public static void ScrollToEnd(this Scrollbar scrollbar)
        {
            scrollbar.value = 1;
        }

        public static void ScrollToStart(this Scrollbar scrollbar)
        {
            scrollbar.value = 0;
        }
    }
}