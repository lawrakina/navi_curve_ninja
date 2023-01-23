using Misc.Fadeable;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelFadeableProvider))]
[CanEditMultipleObjects]
public class LevelFadeableProviderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Setup"))
        {
            var tg = target as LevelFadeableProvider;
            tg.Reset();

            EditorUtility.SetDirty(tg);
        }
    }
}
