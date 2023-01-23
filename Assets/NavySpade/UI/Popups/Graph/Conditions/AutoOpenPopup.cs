using Core.UI.Popups.Abstract;

namespace Core.UI.Popups.Graph.Conditions
{
    [CreateNodeMenu("UI/Popups/Auto open")]
    public class AutoOpenPopup : UIPopupNode<PopupWithCondition>
    {
        [Output] public StateTransition DontAutoOpened;
        
        public override void Run()
        {
            if(Prefab.IsOpen)
                base.Run();
            else
            {
                Complete(GetOutputPort(nameof(DontAutoOpened)));
            }
        }
    }
}