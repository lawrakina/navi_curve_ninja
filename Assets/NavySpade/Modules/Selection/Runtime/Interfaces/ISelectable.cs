using JetBrains.Annotations;

namespace NavySpade.Modules.Selection.Runtime.Interfaces
{
    public interface ISelectable
    {
        [PublicAPI]
        void Select();
        
        [PublicAPI]
        void Deselect();
    }
}