using Misc.Fadeable;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FadeableController))]
[CanEditMultipleObjects]
public class FadeableControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var tg = target as FadeableController;

        if (GUILayout.Button("Setup"))
        {
            tg.Reset();
            EditorUtility.SetDirty(tg);
        }

        if (GUILayout.Button("Fade In"))
        {
            tg.ShowAll();
        }

        if (GUILayout.Button("Fade out"))
        {
            tg.HideAll();
        }
    }
}
