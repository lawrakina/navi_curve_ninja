using UnityEditor;
using UnityEngine;

namespace NavySpade.Modules.Utils.Setters.Editor
{
    [CustomEditor(typeof(PhysicsMaterialSetter))]
    [CanEditMultipleObjects]
    public class PhysicsMaterialSetterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var tg = target as PhysicsMaterialSetter;

            if (GUILayout.Button("Set colliders"))
            {
                tg.Reset();
                EditorUtility.SetDirty(tg);
            }

            if (GUILayout.Button("Set material"))
            {
                tg.SetMaterial();
                EditorUtility.SetDirty(tg);
            }
        }
    }
}