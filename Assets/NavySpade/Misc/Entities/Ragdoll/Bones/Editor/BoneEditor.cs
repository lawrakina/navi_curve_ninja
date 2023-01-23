using Misc.Entities.Ragdoll;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Bone))]
[CanEditMultipleObjects]
public class BoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Setup"))
        {
            var tg = target as Bone;
            tg.Reset();
            EditorUtility.SetDirty(tg);
        }
    }
}