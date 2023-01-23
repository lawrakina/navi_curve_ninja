using JetBrains.Annotations;

namespace NavySpade.Modules.Selection.Runtime.Interfaces
{
    public interface ISelectionCallbackReceiver
    {
        [PublicAPI]
        void OnSelected();

        [PublicAPI]
        void OnDeselected();
    }
}