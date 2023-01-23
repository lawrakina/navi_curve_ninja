using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NavySpade.Modules.Configuration.Editor.Utils;
using NavySpade.Modules.Configuration.Runtime.SO;
using NavySpade.Modules.Configuration.Runtime.Utils;
using NavySpade.Modules.Extensions.CsharpTypes;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace NavySpade.Modules.Configuration.Editor
{
    internal class ObjectConfigWindow : EditorWindow
    {
        private const string WindowName = "Configs";

        private List<ObjectConfig> _configs;
        private string[] _configsNames;
        private bool[] _isFoldout;
        private Vector2 _scrollPos;
        private string _searchRequest;

        private SearchField _searchField;
        private List<int> _matchedIndexes;

        public static void Open()
        {
            var window = (ObjectConfigWindow)GetWindow(typeof(ObjectConfigWindow), false, WindowName);
            window.Show();
        }

        private void CreateGUI()
        {
            RefreshCache();
        }

        private bool IsNeedRefreshCache() =>
            _configs == null || _configs.Any() == false || _configs.Any(x => x == null);

        private void RefreshCache()
        {
            _configs = ObjectConfigLocator.GetAllConfigs();
            _isFoldout = new bool[_configs.Count];
            _searchField = new SearchField();

            SortConfigs();

            _configsNames = new string[_configs.Count];
            for (var i = 0; i < _configs.Count; i++)
            {
                _configsNames[i] = GetConfigNameForSearch(_configs[i]);
            }
        }
        
        private void SortConfigs()
        {
            _configs.Sort(new ConfigOrderComparer());
        }

        private void OnGUI()
        {
            if (IsNeedRefreshCache())
            {
                RefreshCache();
            }

            DrawInfo();

            var newSearchRequest = _searchField?.OnToolbarGUI(_searchRequest);
            var seeAll = newSearchRequest.IsNullOrEmpty();
            if (!newSearchRequest.IsNullOrEmpty() && string.CompareOrdinal(newSearchRequest, _searchRequest) != 0)
            {
                seeAll = TryProcessSearchRequest(newSearchRequest, out _matchedIndexes) == false;
            }

            _searchRequest = newSearchRequest;
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, false, true);

            if (seeAll)
            {
                DrawAllConfigs();
            }
            else
            {
                DrawMatchedConfigs();
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawInfo()
        {
            EditorGUILayout.LabelField($"Count: {_configs.Count}");
        }

        private void DrawAllConfigs()
        {
            for (var index = 0; index < _configs.Count; index++)
            {
                var objectConfig = _configs[index];
                var objectConfigName = ObjectConfigNamingUtility.GetInspectorName(objectConfig);

                _isFoldout[index] = EditorGUILayout.Foldout(_isFoldout[index], objectConfigName);
                if (_isFoldout[index])
                {
                    var serializedObject = new SerializedObject(objectConfig);
                    DrawSerializedObject(serializedObject);
                }
            }
        }

        private void DrawMatchedConfigs()
        {
            foreach (var index in _matchedIndexes)
            {
                var objectConfig = _configs[index];
                var objectConfigName = ObjectConfigNamingUtility.GetInspectorName(objectConfig);

                _isFoldout[index] = EditorGUILayout.Foldout(_isFoldout[index], objectConfigName);
                if (_isFoldout[index])
                {
                    var serializedObject = new SerializedObject(objectConfig);
                    DrawSerializedObject(serializedObject);
                }
            }
        }

        private bool TryProcessSearchRequest(string request, out List<int> matchedIndexes)
        {
            if (request.IsNullOrEmpty() ||
                string.Compare(request, _searchRequest, StringComparison.OrdinalIgnoreCase) == 0)
            {
                matchedIndexes = new List<int>();
                return false;
            }

            matchedIndexes = ProcessSearchRequest(request);
            return true;
        }

        private List<int> ProcessSearchRequest(string request)
        {
            var matchedIndexes = new List<int>();
            var search = ParseSearchRequest(request);

            for (var i = 0; i < _configsNames.Length; i++)
            {
                if (_configsNames[i].Contains(search))
                {
                    matchedIndexes.Add(i);
                }
            }

            return matchedIndexes;
        }

        private static void DrawSerializedObject(SerializedObject serializedObject)
        {
            if (serializedObject == null)
            {
                EditorGUILayout.HelpBox("Target SerializedObject is null!", MessageType.Warning);
                return;
            }

            // Display serializedProperty with selected mode.
            var type = typeof(SerializedObject);
            var propertyInfo = type.GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(serializedObject, InspectorMode.Normal, null);
            }

            var iterator = serializedObject.GetIterator();

            // First call draw target script.
            iterator.NextVisible(true);

            while (iterator.NextVisible(false))
            {
                EditorGUILayout.PropertyField(iterator, true);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private static string GetConfigNameForSearch(ObjectConfig config)
        {
            var configName = ObjectConfigNamingUtility.GetInspectorName(config);
            return ParseSearchRequest(configName);
        }

        private static string ParseSearchRequest(string str)
        {
            return str.ToLower().Replace(" ", "");
        }
    }
}