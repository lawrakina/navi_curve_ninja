using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core.UI.TabSystem.Editor
{
    [CustomEditor(typeof(TabGroup))]
    internal class TabGroupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            if (GUILayout.Button("Setup"))
            {
                var tabGroup = target as TabGroup;

                var buttons = tabGroup.GetComponentsInChildren<TabButton>(true);

                var rootPanel = tabGroup.GetComponent<TabGroupElement>();
                var elements = rootPanel != null
                    ? GetComponentsInChildrenWithout(tabGroup.transform, rootPanel)
                    : tabGroup.GetComponentsInChildren<TabGroupElement>(true);

                tabGroup.Init(buttons, elements);
            }
        }

        private static T[] GetComponentsInChildrenWithout<T>(Component root, T exception)
        {
            var panels = new HashSet<T>(root.GetComponentsInChildren<T>(true));
            panels.Remove(exception);

            return panels.ToArray();
        }
    }
}