using DG.DemiEditor;
using NavySpade._PJ71.Tiles;
using UnityEditor;
using UnityEngine;
// using UnityToolbarExtender;

namespace NavySpade
{
    [InitializeOnLoad]
    public static class TileToolbarExtensions
    {
        // private static TilemapConfig _tilemapConfig;
        // private static string[] _teamNames;
        //
        // static TileToolbarExtensions()
        // {
        //     ToolbarExtender.LeftToolbarGUI.Add(DrawLeftGUI);
        //     _tilemapConfig = TilemapConfig.Instance;
        //     UpdateTeamNames();
        // }
        //
        // private static void DrawLeftGUI()
        // {
        //     GUILayout.FlexibleSpace();
        //     _tilemapConfig.SelectedIndex = EditorGUILayout.Popup(_tilemapConfig.SelectedIndex, _teamNames);
        //     if (GUILayout.Button("UpdateTeams"))
        //     {
        //         string[] guids = AssetDatabase.FindAssets(
        //             $"t:{typeof(TileTeamAttachment)}",
        //             new []{ TilemapConfig.PathToTileTeams});
        //
        //         _tilemapConfig.TileTeams = new TileTeamAttachment[guids.Length];
        //         for (int i = 0; i < guids.Length; i++)
        //         {
        //             string path = AssetDatabase.GUIDToAssetPath(guids[i]);
        //             _tilemapConfig.TileTeams[i] = AssetDatabase.LoadAssetAtPath<TileTeamAttachment>(path);
        //         }
        //         
        //         UpdateTeamNames();
        //     }
        // }
        //
        // private static void UpdateTeamNames()
        // {
        //     _teamNames = new string [_tilemapConfig.TileTeams.Length];
        //     for (int i = 0; i < _teamNames.Length; i++)
        //     {
        //         if(_tilemapConfig.TileTeams[i] == null)
        //             continue;
        //         
        //         _teamNames[i] = _tilemapConfig.TileTeams[i].name;
        //     }
        // }
    }
}
