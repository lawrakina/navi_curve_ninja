using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Currencies
{
    /// <summary>
    /// Эта валюта не сохраняется.
    /// </summary>
    [CreateAssetMenu(fileName = "New currency", menuName = "Meta/Currency/Runtime", order = 0)]
    [HelpURL("https://navyspade.atlassian.net/wiki/spaces/BP/pages/11698177")]
    public class RuntimeCurrency : Currency
    {
        private int? _count;

        public override int Count
        {
            get
            {
                _count ??= StartCount;
                return _count.Value;
            }
            set
            {
                _count ??= StartCount;
                
                var different = value - _count.Value;
                _count = value;
                
                InvokeCountChangeEvent(different);
            }
        }
    }
}