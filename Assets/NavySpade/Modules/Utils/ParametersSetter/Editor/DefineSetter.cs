using System.Linq;
using UnityEditor;

namespace NavySpade.Modules.Utils.ParametersSetter.Editor
{
    public static class DefineSetter
    {
        public static void SetData(string value)
        {
            var defs = PlayerSettings.GetScriptingDefineSymbolsForGroup(
                EditorUserBuildSettings.selectedBuildTargetGroup);
            var splitedDefs = defs.Split(';').ToList();

            if (splitedDefs.Contains(value) == false)
            {
                splitedDefs.Add(value);
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", splitedDefs.ToArray()));
        }
    }
}