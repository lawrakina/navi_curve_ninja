using System;
using Core.Visual;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.Modules.Visual.Runtime.Data;

namespace NavySpade.Modules.Visual.Runtime
{
    public static class VisualManager
    {
        private const string SaveKey = "SELECTED_VISUAL_VARIANT";
        
        public static event Action<VisualData> OnChangeVisual;

        private static VisualConfig _config;
        private static VisualConfig Config => _config == null ? _config = VisualConfig.Instance : _config;

        public static VisualData SelectedVisual
        {
            get
            {
                var index = SaveManager.Load(SaveKey, -1);
                return index == -1 ? Config.SelectedFromStart : Config.AllVisualVatiantsInGame[index];
            }
            set
            {
                var valueIndex = -1;
                for (var i = 0; i < Config.AllVisualVatiantsInGame.Length; i++)
                {
                    var item = Config.AllVisualVatiantsInGame[i];
                    if (item == value)
                    {
                        valueIndex = i;
                        break;
                    }
                }

                if (valueIndex == -1)
                {
                    throw new Exception($"Виузал {value.name} не задан в конфигах как используемый в игре!");
                }

                SaveManager.Save(SaveKey, valueIndex);
                OnChangeVisual?.Invoke(value);
            }
        }
        
        public static void NextVisual()
        {
            var start = SelectedVisual;
            var startIndex = 0;

            for (var i = 0; i < Config.AllVisualVatiantsInGame.Length; i++)
            {
                if (Config.AllVisualVatiantsInGame[i] == start)
                {
                    startIndex = i;
                    break;
                }
            }

            startIndex++;

            if (startIndex >= Config.AllVisualVatiantsInGame.Length)
            {
                startIndex = 0;
            }

            SelectedVisual = Config.AllVisualVatiantsInGame[startIndex];
        }
    }
}