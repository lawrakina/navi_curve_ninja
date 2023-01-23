namespace Utils.TriggerAction.Triggers.Mono
{
    /// <summary>
    /// Fires the action immediately when the gameObject fires its Update() callback.
    /// </summary>
    public class UpdateMonoTrigger : Base.MonoTrigger
    {
        private void Update()
        {
            FireAction();
        }
    }
}