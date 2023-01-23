using System.Linq;
using Misc.Entities.Ragdoll;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using Bone = Misc.Entities.Ragdoll.Bone;

[CustomEditor(typeof(RagdollHandler))]
[CanEditMultipleObjects]
public class RagdollHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        
        if (GUILayout.Button("Setup"))
        {
            var tg = target as RagdollHandler;
            var bones = tg.GetComponentsInChildren<Bone>().ToList();
            
            foreach (var bone in bones)
            {
                if (bone.Physics == null)
                    continue;
                
                bone.Physics.mainHandler = tg;
                EditorUtility.SetDirty(bone);
            }

            tg.bones = bones;

            EditorUtility.SetDirty(tg);
        }
    }
}
