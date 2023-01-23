namespace NavySpade.Modules.Saving.Runtime.Interfaces
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}