using NavySpade.Modules.Configuration.Editor;
using NavySpade.Modules.StateMachines.Runtime.Mono;
using UnityEditor;
using UnityEngine;

namespace NavySpade.Modules.StateMachines.Editor
{
    [CustomEditor(typeof(StateBehavior), true)]
    public class StateEditor : UnityEditor.Editor
    {
        private StateBehavior _target;

        private void OnEnable()
        {
            _target = target as StateBehavior;
        }

        //Inspector GUI:
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (Application.isPlaying == false)
            {
                GUILayout.BeginHorizontal();
                
                DrawSoloButton();
                DrawHideAllButton();
                
                GUILayout.EndHorizontal();
            }
        }

        private void DrawHideAllButton()
        {
            GUI.color = EditorConfig.Instance.InvalidColor;
            
            if (GUILayout.Button("Hide"))
            {
                Undo.RegisterCompleteObjectUndo(_target.transform.parent.transform, "Hide");
                
                _target.gameObject.SetActive(false);
            }
        }

        private void DrawSoloButton()
        {
            GUI.color = EditorConfig.Instance.ValidColor;
            
            if (GUILayout.Button("Solo"))
            {
                foreach (Transform item in _target.transform.parent.transform)
                {
                    if (item != _target.transform)
                    {
                        item.gameObject.SetActive(false);
                    }
                    
                    Undo.RegisterCompleteObjectUndo(_target, "Solo");
                }
                
                _target.gameObject.SetActive(true);
            }
        }
    }
}