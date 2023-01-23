using UnityEngine;

namespace Core.UI.Main
{
    public class SelectionContext : MonoBehaviour
    {
        public SelectionItem PreviewSelected { get; private set; }

        public void Select(SelectionItem item)
        {
            Deselect(PreviewSelected);
            
            item.Selected.Invoke();
            PreviewSelected = item;
        }

        public void Deselect(SelectionItem item)
        {
            if(item != null)
                item.Deselected.Invoke();
        }
    }
}