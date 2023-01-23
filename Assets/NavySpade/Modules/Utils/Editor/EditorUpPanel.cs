using Core.Editor.DeveloperTools;
using UnityEditor;
using Utils;

public static class EditorUpPanel
{
    [MenuItem("Tools/Screenshot _HOME")]
    public static void TakeScreenshot()
    {
        ScreenShooter.TakeScreenshot();
    }
}
