using System.Collections.Generic;
using NavySpade.Modules.Configuration.Runtime.SO;
using UnityEngine;

namespace NavySpade.Meta.Runtime.Economic.Currencies
{
    public class CurrencyConfig : ObjectConfig<CurrencyConfig>
    {
        [field: SerializeField] public List<Currency> UsedInGame { get; private set; }
    }
}