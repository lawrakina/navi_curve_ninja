namespace Core.Meta.Shop.Selectors
{
    public interface ISelected
    {
        void Select();
        void Deselect();
        bool IsSelected();
    }
}