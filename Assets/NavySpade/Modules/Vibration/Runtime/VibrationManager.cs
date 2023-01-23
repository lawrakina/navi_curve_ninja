namespace NavySpade.Modules.Vibration.Runtime
{
    public enum VibrationType
    {
        Light,
        Medium,
        Hard
    }
    
    /// <summary>
    /// Global context. Just for easier syntax. Uses inside <see cref="IVibrationService"/>.
    /// </summary>
    public static class VibrationManager
    {
        private static IVibrationService GlobalService => VibrationConfig.Instance.Service;
        
        public static void VibrateLight()
        {
            GlobalService.VibrateLight();
        }

        public static void VibrateMedium()
        {
            GlobalService.VibrateMedium();
        }

        public static void VibrateHard()
        {
            GlobalService.VibrateHard();
        }

        public static void CancelAll()
        {
            GlobalService.CancelAll();
        }

        public static void Vibrate(VibrationType type)
        {
            switch (type)
            {
                case VibrationType.Light:
                    GlobalService.VibrateLight();
                    break;
                case VibrationType.Medium:
                    GlobalService.VibrateMedium();
                    break;
                case VibrationType.Hard:
                    break;
            }
        }
    }
}