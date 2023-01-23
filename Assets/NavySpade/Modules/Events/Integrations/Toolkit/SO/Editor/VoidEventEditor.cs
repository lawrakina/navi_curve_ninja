using EventSystem.Integrations.Toolkit.SO.Events;
using UnityEditor;
using UnityEngine;

namespace Depra.EventSystem.Integrations.Toolkit.SO.Editor
{
    [CustomEditor(typeof(VoidGameEvent))]
    public class VoidEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUI.enabled = Application.isPlaying;

            if (GUILayout.Button("Invoke"))
            {
                var gameEvent = target as VoidGameEvent;
                gameEvent.Invoke();
            }
        }
    }
}