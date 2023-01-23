using NavySpade.Modules.Visual.Runtime;
using UnityEditor;

namespace NavySpade.Modules.Visual.Editor
{
    public static class VisualUtility
    {
        [MenuItem("Tools/Debug/Next Visual")]
        public static void SetNextVisual()
        {
            VisualManager.NextVisual();
        }
    }
}