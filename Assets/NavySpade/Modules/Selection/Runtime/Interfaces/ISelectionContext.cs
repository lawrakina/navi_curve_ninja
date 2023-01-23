using JetBrains.Annotations;

namespace NavySpade.Modules.Selection.Runtime.Interfaces
{
    public interface ISelectionContext<in T>
    {
        [PublicAPI]
        void Select(T item);

        [PublicAPI]
        void Deselect(T item);
    }
}