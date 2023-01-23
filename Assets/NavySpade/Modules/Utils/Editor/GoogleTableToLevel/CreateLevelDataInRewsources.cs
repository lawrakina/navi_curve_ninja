using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

public static class CreateLevelDataInRewsources 
{

    [MenuItem("Tools/DownloadAndParseTable")]
    public static void DownloadTableAndParse()
    {
        //

        Unity.EditorCoroutines.Editor.EditorCoroutineUtility.StartCoroutineOwnerless(parserDownloader());
    }

    public static IEnumerator parserDownloader()
    {
        var path = Application.dataPath.Replace("Assets", "DataTable");
        Debug.LogError(path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
#pragma warning disable 618
        var dw = new WWW("https://docs.google.com/spreadsheets/d/e/2PACX-1vQNdEmRvl4Q46k3mhXlgLE37kWVSfETHwf_9Kggxr7bKB-SyoU3bsRbsD8vAWsPBARAPGbigSa923R4/pub?gid=0&single=true&output=csv");
#pragma warning restore 618
        yield return dw;
        var filePath = path + "/" + "TableData.csv";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        File.WriteAllText(filePath, dw.text);
        ParseLevels();
    }

    
    public static void ParseLevels()
    {
        var path = Application.dataPath.Replace("Assets", "DataTable");
        var filePath = path + "/" + "TableData.csv";
        var lines = File.ReadAllLines(filePath);
        Dictionary<string, List<List<string>>> levels = new Dictionary<string, List<List<string>>>();
        int num = 0;
        for (int i = 1; i < lines.Length; i++)
        {
            var strValue = lines[i];
            var levelsParams = strValue.Split(',');
            var key = levelsParams[0];
            if (!levels.ContainsKey(key))
            {
                levels.Add(key , new List<List<string>>());
                num = 0;
            }
            else
            {
                num++;
            }
            
            for (int j = 1; j < levelsParams.Length; j++)
            {
                var param = levelsParams[j];
                if(string.IsNullOrEmpty(param)) break;
                if(levels[key].Count<=num) levels[key].Add(new List<string>());
                levels[key][num].Add(param);
            }
        }

        
        //CreateLevelData(levelName, columns);
    }
    public static void CreateLevelData(string name/*, List<Columns> columns*/)
    {
        var path = "Assets/Resources/LevelsData/" + name + ".asset";
       /* if (File.Exists(Application.dataPath.Replace("Assets", path)))
        {
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.Refresh();
        }*/
        //var le = ScriptableObject.CreateInstance<LevelBlock>();
        //le.columns = columns;
        //EditorUtility.SetDirty(le);
        if (!AssetDatabase.IsValidFolder("Assets/Resources/LevelsData"))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "LevelsData");
            AssetDatabase.Refresh();
        }
        
      //  AssetDatabase.CreateAsset(le, path);
        AssetDatabase.Refresh();
    }
    
}
