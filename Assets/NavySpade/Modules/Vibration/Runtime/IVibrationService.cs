namespace NavySpade.Modules.Vibration.Runtime
{
    public interface IVibrationService
    {
        void VibrateLight();
        void VibrateMedium();
        void VibrateHard();

        void CancelAll();
    }
}