using Misc.RootProviders.Runtime.Base;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace Core.UI.TabSystem
{
    public class TabGroupElement : MonoBehaviour
    {
        [SR] [SerializeReference] private RootProvider _root;

        public void Show()
        {
            _root.TryShow();
        }

        public void Hide()
        {
            _root.TryHide();
        }
    }
}