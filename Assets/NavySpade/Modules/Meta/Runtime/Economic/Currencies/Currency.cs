using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Currencies
{
    [CreateAssetMenu(fileName = "New Currency", menuName = "Meta/Currency/Base", order = 0)]
    [HelpURL("https://navyspade.atlassian.net/wiki/spaces/BP/pages/11698177")]
    public class Currency : ScriptableObject
    {
        [field: SerializeField] public int StartCount { get; private set; }
        [field: SerializeField] public Sprite ShopIcon { get; private set; }

        public delegate void CurrencyDifferentChange(int different);
        public event CurrencyDifferentChange CountChanged;
        
        protected string PrefsKey => $"Currency.{name}";

        public virtual int Count
        {
            get => SaveManager.Load(PrefsKey, StartCount);
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                SaveManager.Save(PrefsKey, value);
                InvokeCountChangeEvent(value - Count);
            }
        }

        protected void InvokeCountChangeEvent(int different)
        {
            CountChanged?.Invoke(different);
        }
    }
}