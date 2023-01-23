using System.Collections.Generic;
using NavySpade.Modules.Utils.Singletons.Runtime.Core.Attributes;
using UnityEditor;
using UnityEngine;

namespace NavySpade.Modules.Utils.Singletons.Editor
{
    public class SingletonsWindow : EditorWindow
    {
        private readonly struct Data
        {
            public string Name { get; }

            public Data(string name)
            {
                Name = name;
            }
        }

        private List<Data> _allData;
        private Vector2 _scrollPos;

        [MenuItem("Tools/Singletons/Inspector", priority = 2)]
        private static void ShowWindow()
        {
            var window = GetWindow<SingletonsWindow>();

            window.RefreshAllData();

            window.titleContent = new GUIContent("Singletons Inspector");
            window.Show();
        }

        private void OnGUI()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, false, true);

            if (_allData == null)
            {
                RefreshAllData();
            }

            foreach (var data in _allData)
            {
                DrawData(data);
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawData(Data data)
        {
            DrawSingleton(data);
        }

        private void DrawSingleton(Data singleton)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.TextField("Name:", singleton.Name);

            EditorGUILayout.EndHorizontal();
        }

        private void RefreshAllData()
        {
            var registered = SingletonAttribute.GetListOfSingletonClassNames();
            _allData = new List<Data>(registered.Count);

            foreach (var type in registered)
            {
                _allData.Add(new Data(type));
            }
        }
    }
}