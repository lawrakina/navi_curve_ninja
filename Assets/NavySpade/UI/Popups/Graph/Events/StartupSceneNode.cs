namespace Core.UI.Popups.Graph.Events
{   
    [CreateNodeMenu("Core/Startup scene")]
    public class StartupSceneNode : State
    {
        [Output] public StateTransition Output;
        
        public override void Run()
        {
            Complete(GetOutputPort(nameof(Output)));
        }
    }
}