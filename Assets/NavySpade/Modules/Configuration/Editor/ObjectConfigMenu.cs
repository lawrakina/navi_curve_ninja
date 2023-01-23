using UnityEditor;

namespace NavySpade.Modules.Configuration.Editor
{
    internal static class ObjectConfigMenu
    {
        [MenuItem("Tools/Configurations", priority = 0)]
        public static void Open()
        {
            ObjectConfigWindow.Open();
        }
    }
}