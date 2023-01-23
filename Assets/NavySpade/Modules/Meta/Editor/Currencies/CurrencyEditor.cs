using NavySpade.Meta.Runtime.Economic.Currencies;
using UnityEditor;
using UnityEngine;

namespace NavySpade.Meta.Editor.Currencies
{
    [CustomEditor(typeof(Currency), true)]
    public class CurrencyEditor : UnityEditor.Editor
    {
        private static Color? _defaultColor;

        public override void OnInspectorGUI()
        {
            _defaultColor ??= GUI.color;

            var config = CurrencyConfig.Instance;
            var targetAsCurrency = target as Currency;

            var isContains = config.UsedInGame.Contains(targetAsCurrency);
            var buttonText = isContains == false ? "Mark As Used In Game" : "Remove From Game";

            GUI.color = isContains ? _defaultColor.Value : Color.red;
            if (GUILayout.Button(buttonText))
            {
                if (isContains)
                {
                    config.UsedInGame.Remove(targetAsCurrency);
                }
                else
                {
                    config.UsedInGame.Add(targetAsCurrency);
                }

                EditorUtility.SetDirty(config);
            }

            GUI.color = _defaultColor.Value;

            base.OnInspectorGUI();
        }
    }
}