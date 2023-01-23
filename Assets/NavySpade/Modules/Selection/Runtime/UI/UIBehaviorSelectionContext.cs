using NavySpade.Modules.Selection.Runtime.Abstract;

namespace NavySpade.Modules.Selection.Runtime.UI
{
    public class UIBehaviorSelectionContext : SelectionContext<SelectableUIBehavior>
    {
        public SelectableUIBehavior PreviousSelected { get; private set; }

        public override void Select(SelectableUIBehavior item)
        {
            Deselect(PreviousSelected);
            
            item.Selected.Invoke();
            PreviousSelected = item;
        }

        public override void Deselect(SelectableUIBehavior item)
        {
            if (item != null)
            {
                item.Deselected.Invoke();
            }
        }
    }
}