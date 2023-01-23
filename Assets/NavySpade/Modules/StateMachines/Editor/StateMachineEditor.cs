using NavySpade.Modules.Configuration.Editor;
using NavySpade.Modules.StateMachines.Runtime.Mono;
using UnityEditor;
using UnityEngine;
using StateMachineBehaviour = NavySpade.Modules.StateMachines.Runtime.Mono.StateMachineBehaviour;

namespace NavySpade.Modules.StateMachines.Editor
{
    [CustomEditor(typeof(StateMachineBehaviour), true)]
    public class StateMachineEditor : UnityEditor.Editor
    {
        private StateMachineBehaviour _target;

        private static EditorConfig Config => EditorConfig.Instance;

        private void OnEnable()
        {
            _target = target as StateMachineBehaviour;
        }

        public override void OnInspectorGUI()
        {
            // If no states are found:
            if (_target.transform.childCount == 0)
            {
                DrawNotification("Add child GameObjects for this State Machine to control.", Color.yellow);
                return;
            }

            base.OnInspectorGUI();
            
            // Change buttons:
            if (EditorApplication.isPlaying)
            {
                DrawStateChangeButtons();
            }
            else
            {
                DrawHideAllButton();
            }
        }

        private void DrawStateChangeButtons()
        {
            if (_target.transform.childCount == 0)
            {
                return;
            }
            
            var currentColor = GUI.color;

            var states = _target.GetComponentsInChildren<StateBehavior>(true);
            foreach (var currentState in states)
            {
                if (_target.CurrentState != null && currentState == _target.CurrentState)
                {
                    GUI.color = Config.ValidColor;
                }
                else
                {
                    GUI.color = Config.NormalColor;
                }

                if (GUILayout.Button(currentState.name))
                {
                    _target.ChangeState(currentState);
                }
            }

            GUI.color = currentColor;
            
            if (GUILayout.Button("Exit"))
            {
                _target.Exit();
            }
        }

        private void DrawHideAllButton()
        {
            GUI.color = HasActiveChildren(_target.transform) ? Config.InvalidColor : Config.NormalColor;
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Hide All"))
            {
                Undo.RegisterCompleteObjectUndo(_target.transform, "Hide All");

                foreach (Transform item in _target.transform)
                {
                    item.gameObject.SetActive(false);
                }
            }

            GUILayout.EndHorizontal();
        }

        private void DrawNotification(string message, Color color)
        {
            var currentColor = GUI.color;
            GUI.color = color;
            EditorGUILayout.HelpBox(message, MessageType.Warning);
            GUI.color = currentColor;
        }

        private bool HasActiveChildren(Transform parent)
        {
            for (var i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i).gameObject.activeInHierarchy)
                {
                    return true;
                }
            }

            return false;
        }
    }
}