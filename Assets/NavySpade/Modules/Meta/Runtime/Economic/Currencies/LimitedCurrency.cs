using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Currencies
{
    [CreateAssetMenu(fileName = "New currency", menuName = "Meta/Currency/Limited", order = 0)]
    [HelpURL("https://navyspade.atlassian.net/wiki/spaces/BP/pages/11698177")]
    public class LimitedCurrency : Currency
    {
        [field: Min(0), SerializeField] public int MaxCount { get; private set; }

        public override int Count
        {
            get => base.Count;
            set
            {
                if (value > MaxCount)
                {
                    value = MaxCount;
                }

                base.Count = value;
            }
        }
    }
}