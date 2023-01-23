namespace NavySpade.Modules.Extensions
{
    public static class IntExtensions
    {
        public static string ConvertSeconds(int time)
        {
            var minutes = time / 60;
            var minutesText = "";
            
            var seconds = time % 60;
            var secondsText = "";

            if (minutes > 0)
            {
                minutesText = minutes + " minute" + (minutes > 1 ? "s " : " ");
            }

            if (seconds > 0)
            {
                secondsText = seconds + " second" + (seconds > 1 ? "s " : " ");
            }

            return (minutesText + secondsText).Substring(0, (minutesText + secondsText).Length - 1);
        }
    }
}