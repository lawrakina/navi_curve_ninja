using NavySpade.UI.Popups.Abstract;
using UnityEngine;

namespace Core.UI.Popups.Abstract
{
    public abstract class PopupWithCondition : Popup
    {
        public abstract bool IsOpen { get; }
    }
}