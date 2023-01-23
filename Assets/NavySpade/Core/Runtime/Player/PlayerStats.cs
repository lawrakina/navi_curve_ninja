using System.Globalization;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace Core.Player
{
    public static class PlayerStats
    {
        //TODO: use Analytic for this fields
        public static double PlaysCount
        {
            get => double.Parse(SaveManager.Load("PlaysCount", "0"));
            set => SaveManager.Save("PlaysCount", value.ToString(CultureInfo.InvariantCulture));
        }

        public static double WinsCount
        {
            get => double.Parse(SaveManager.Load("WinsCount", "0"));
            set => SaveManager.Save("WinsCount", value.ToString(CultureInfo.InvariantCulture));
        }
    }
}