namespace Core.Extensions.CsharpTypes
{
    public static class IntExtensions
    {
        public static string ConvertSeconds(int time)
        {
            var _minutes = time / 60;
            string minutes = "";
            int _seconds = time % 60;
            string seconds = "";

            if (_minutes > 0)
            {
                minutes = _minutes + " minute" + (_minutes > 1 ? "s " : " ");
            }

            if (_seconds > 0)
            {
                seconds = _seconds + " second" + (_seconds > 1 ? "s " : " ");
            }

            return (minutes + seconds).Substring(0, (minutes + seconds).Length - 1);
        }
    }
}