using Main.Levels;
using Main.Saving;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

public static class GlobalParameters
{
    public static int SkinID{
        get => SaveManager.Load("SkinIDG", 0);
        set => SaveManager.Save("SkinIDG", value);
    }

    public static double DoubleLevelNumber
    {
        get => double.Parse(SaveManager.Load("LEVELNUM", "1"));
        set => SaveManager.Save("LEVELNUM", value.ToString());
    }

    public static int AttemptCount
    {
        get => SaveManager.Load($"ATTEMPT_COUNT_{LegacyLevelManager.CurrentLevelIndex}", 0);
        set => SaveManager.Save($"ATTEMPT_COUNT_{LegacyLevelManager.CurrentLevelIndex}", value);
    }
}
