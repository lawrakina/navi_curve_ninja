using Core.Visual;
using NavySpade.Modules.Visual.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace NavySpade.Modules.Visual.Editor
{
    [CustomEditor(typeof(VisualConfig))]
    public class VisualConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set all"))
            {
                var tg = target as VisualConfig;
                tg.SetVisuals(AssetUtils.GetAllScriptableObjects<VisualData>().ToArray());
                EditorUtility.SetDirty(tg);
            }
        }
    }
}
