using UnityEditor;
using UnityEngine;
using Utils.SO.Events.Events;

namespace Utils.SO.Events.Editor
{
    [CustomEditor(typeof(VoidEvent))]
    public class VoidEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            if (GUILayout.Button("Invoke"))
            {
                var gameEvent = target as VoidEvent;
                gameEvent.Invoke();
            }
        }
    }
}