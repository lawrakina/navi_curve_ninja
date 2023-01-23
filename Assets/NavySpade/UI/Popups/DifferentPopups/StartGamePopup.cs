using Core.UI.Popups.Abstract;
using NavySpade.UI.Popups.Abstract;

namespace NavySpade.UI.Popups.DifferentPopups
{
    public class StartGamePopup : PopupWithCondition
    {
        public static StartGamePopup Instance { get; private set; }

        public override void OnAwake()
        {
            Instance = this;
        }

        public override void OnStart()
        {
            
        }

        public override bool IsOpen{ get; }
    }
}