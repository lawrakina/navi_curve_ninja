using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// For editor use only!
/// </summary>
public static class AssetUtils
{
    private const string PrefabPostfix = ".prefab";
    private const string ScriptableObjetPostfix = ".asset";

    public static List<T> GetAllPrefabs<T>() where T : Object
    {
        return GetAllObjects<T>(PrefabPostfix);
    }

    public static List<T> GetAllScriptableObjects<T>() where T : Object
    {
        return GetAllObjects<T>(ScriptableObjetPostfix);
    }

    private static List<T> GetAllObjects<T>(string type) where T : Object
    {
        var objects = new List<T>();
        var paths = AssetDatabase.GetAllAssetPaths();

        foreach (var path in paths)
        {
            if (path.Contains(type) == false)
                continue;

            var prefab = AssetDatabase.LoadAssetAtPath<T>(path);
            if (prefab == null)
                continue;

            objects.Add(prefab);
        }

        return objects;
    }
}
