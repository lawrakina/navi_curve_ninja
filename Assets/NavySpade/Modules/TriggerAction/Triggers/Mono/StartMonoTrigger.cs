namespace Utils.TriggerAction.Triggers.Mono
{
    /// <summary>
    /// Fires the action immediately when the gameObject fires its Start() callback.
    /// </summary>
    public class StartMonoTrigger : Base.MonoTrigger
    {
        private void Start()
        {
            FireAction();
        }
    }
}