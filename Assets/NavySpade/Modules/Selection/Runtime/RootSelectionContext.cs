using Depra.Toolkit.RootProviders.Runtime.Behaviors;
using NavySpade.Modules.Selection.Runtime.Abstract;

namespace NavySpade.Modules.Selection.Runtime
{
    public class RootSelectionContext : SelectionContext<RootBehavior>
    {
        public RootBehavior PreviousSelected { get; private set; }

        public override void Select(RootBehavior item)
        {
            Deselect(PreviousSelected);
            
            item.Show();
            PreviousSelected = item;
        }

        public override void Deselect(RootBehavior item)
        {
            if (item != null)
            {
                item.Hide();
            }
        }
    }
}