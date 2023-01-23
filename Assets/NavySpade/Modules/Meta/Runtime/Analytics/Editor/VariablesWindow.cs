using System;
using NavySpade.Meta.Runtime.Analytics;
using UnityEditor;
using UnityEngine;

namespace Core.Meta.Analytics.Editor
{
    public class VariablesWindow : EditorWindow
    {
        private static NonInstalledVariableData[] _datas;

        private TrackingVariableType _selectedType;
        private float _value;
        private string _key;

        [MenuItem("Window/Analysis/Tracking Variables Debug")]
        private static void ShowWindow()
        {
            var window = GetWindow<VariablesWindow>();
            window.titleContent = new GUIContent("Tracking Variables");
            window.Show();
        }

        private void OnGUI()
        {
            if (_datas == null)
                _datas = AttributeCollector.GetDatas();
            
            EditorGUILayout.BeginVertical();

            DrawDebugAdd();

            foreach (var pair in VariableTracker.Datas)
            {
                DrawData(pair.Value);
            }


            foreach (var nonInstalledVariableData in _datas)
            {
                if(VariableTracker.Datas.ContainsKey(nonInstalledVariableData.DefaultKey) == false)
                    DrawNonInstalledData(nonInstalledVariableData);
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawDebugAdd()
        {
            EditorGUILayout.BeginHorizontal();
            {
                _key = EditorGUILayout.TextField("key:", _key);
                _value = EditorGUILayout.FloatField("value:", _value);
                _selectedType = (TrackingVariableType)EditorGUILayout.EnumPopup("type:", _selectedType);
                
                if (GUILayout.Button("add value"))
                {
                    switch (_selectedType)
                    {
                        case TrackingVariableType.Int:
                            VariableTracker.UpdateValue(_key, (int)_value);
                            break;
                        case TrackingVariableType.Float:
                            VariableTracker.UpdateValue(_key, (float)_value);
                            break;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawData(VariableData data)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.TextField(data.Key);

                EditorGUILayout.FloatField("current value:", data.CurrentValue);
                EditorGUILayout.FloatField("max value:", data.MaxValue);
                EditorGUILayout.FloatField("add value:", data.AddValue);
                EditorGUILayout.FloatField("spended value:", data.ReducedValue);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawNonInstalledData(NonInstalledVariableData data)
        {
            GUI.backgroundColor = Color.yellow;
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.TextField(data.DefaultKey);

                GUILayout.Label("not installed");
                EditorGUILayout.FloatField("current value:", -1);
                EditorGUILayout.FloatField("max value:", -1);
                EditorGUILayout.FloatField("add value:", -1);
                EditorGUILayout.FloatField("spended value:", -1);
            }
            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = Color.clear;
        }
    }
}