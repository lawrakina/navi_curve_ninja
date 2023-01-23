using NavySpade.Meta.Runtime.Economic.Currencies;
using UnityEditor;

namespace NavySpade.Meta.Editor.Currencies
{
    public static class CurrencyEditorTools
    {
        [MenuItem("Tools/Meta/Add 100 to selected currency")]
        public static void Add100SelectedCurrency()
        {
            if (Selection.activeObject is Currency currency)
            {
                currency.Count += 100;
            }
        }
    }
}