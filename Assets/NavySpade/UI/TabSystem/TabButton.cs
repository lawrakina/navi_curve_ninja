using Depra.Toolkit.RootProviders.Runtime.Base;
using Misc.RootProviders.Runtime.Base;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI.TabSystem
{
    [RequireComponent(typeof(Image))]
    [DisallowMultipleComponent]
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SR] [SerializeReference] private RootProvider _root;
        
        [SerializeField] private UnityEvent _onTabEnter = new UnityEvent();
        [SerializeField] private UnityEvent _onTabExit = new UnityEvent();
        [SerializeField] private UnityEvent _onTabSelected = new UnityEvent();
        [SerializeField] private UnityEvent _onTabDeselected = new UnityEvent();

        public int Index { get; private set; }
        public Image Background
        {
            get
            {
                if (_background == null)
                    _background = GetComponent<Image>();

                return _background;
            }
        }

        private Image _background = null;
        private bool _isSelected = false;

        private TabGroup _tabGroup;

        public void Init(TabGroup tabGroup, int index)
        {
            _tabGroup = tabGroup;
            Index = index;

            Background.color = _tabGroup.TabIdleColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onTabEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isSelected == false)
                _onTabExit.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _tabGroup.OnTabSelected(this);
        }

        public void Select()
        {
            SetColor(_tabGroup.TabActiveColor);
            _isSelected = true;

            _onTabSelected.Invoke();
        }

        public void Deselect()
        {
            SetColor(_tabGroup.TabIdleColor);
            _isSelected = false;

            _onTabDeselected.Invoke();
        }

        private void SetColor(Color color)
        {
            Background.color = color;
        }
    }
}