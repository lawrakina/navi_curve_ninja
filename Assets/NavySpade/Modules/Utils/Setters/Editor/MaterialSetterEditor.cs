using UnityEditor;
using UnityEngine;

namespace NavySpade.Modules.Utils.Setters.Editor
{
    [CustomEditor(typeof(MaterialSetter))]
    [CanEditMultipleObjects]
    public class MaterialSetterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set material"))
            {
                var tg = target as MaterialSetter;
                tg.SetMaterial();

                EditorUtility.SetDirty(tg);
            }
        }
    }
}
