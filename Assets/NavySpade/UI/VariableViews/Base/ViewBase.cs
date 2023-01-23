using Misc.RootProviders.Runtime.Base;
using UnityEngine;

namespace NavySpade.UI.VariableViews.Base
{
    public abstract class ViewBase : MonoBehaviour
    {
        [field: SerializeReference, SubclassSelector]
        public RootProvider Root { get; private set; }
        
        public virtual void Enable() => Root.TryShow();

        public virtual void Disable() => Root.TryHide();
    }
}
