using System.Linq;
using Misc.RootProviders.Runtime.Base;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace Core.UI.TabSystem
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private TabButton[] _tabButtons;
        [SerializeField] private TabGroupElement[] _elements;
        [SR] [SerializeReference] private RootProvider _temp;

        [SerializeField] private Color _tabIdleColor = Color.white;
        [SerializeField] private Color _tabActiveColor = Color.red;

        [SerializeField] private bool _isCanBeInactive;

        public Color TabIdleColor => _tabIdleColor;
        public Color TabActiveColor => _tabActiveColor;

        private TabButton _activeTab;
        private TabButton _lastSelectedTab;

        private void Awake()
        {
            for (var i = 0; i < _tabButtons.Length; i++)
            {
                _tabButtons[i].Init(this, i);
            }
        }

        private void Start()
        {
            if (_isCanBeInactive == false)
                OnTabSelected(_tabButtons[0]);
        }

        private void OnEnable()
        {
            if (_isCanBeInactive == false && _activeTab == null)
            {
                if (_lastSelectedTab)
                {
                    OnTabSelected(_lastSelectedTab);
                }
                else
                {
                    ShowNextTab();
                }
            }
        }

        public void OnTabSelected(TabButton button)
        {
            if (_activeTab)
                _activeTab.Deselect();

            if (button == null)
                return;

            var index = button.Index;

            if (_activeTab == button && _isCanBeInactive)
            {
                _activeTab = null;
                button.Deselect();

                _elements[index].Hide();
            }
            else
            {
                _activeTab = button;
                _activeTab.Select();
                _lastSelectedTab = _activeTab;

                for (var i = 0; i < _elements.Length; i++)
                {
                    if (i == index)
                        _elements[i].Show();
                    else
                        _elements[i].Hide();
                }
            }

            ResetTabs();
        }

        public void ShowNextTab()
        {
            if (_tabButtons.Length < 1)
                return;

            int newIndex;

            if (_activeTab == null)
                newIndex = 0;
            else
                newIndex = _activeTab.Index + 1;

            if (newIndex >= _tabButtons.Length)
                newIndex = 0;

            OnTabSelected(_tabButtons[newIndex]);
        }

        public void ShowPreviousTab()
        {
            if (_tabButtons.Length < 1)
                return;

            var newIndex = _activeTab.Index - 1;

            if (newIndex < 0)
                newIndex = _tabButtons.Length - 1;

            OnTabSelected(_tabButtons[newIndex]);
        }

        public void Init(TabButton[] buttons, TabGroupElement[] panels)
        {
            _tabButtons = buttons;
            _elements = panels;
        }

        private void ResetTabs()
        {
            foreach (TabButton button in _tabButtons.Where(button => _activeTab == null || button != _activeTab))
            {
                button.Deselect();
            }
        }

        private void OnPanelClosed(TabGroupElement panel)
        {
            if (_activeTab == null)
                return;

            var closedPanel = _elements[_activeTab.Index];

            if (panel != closedPanel)
                return;

            _activeTab.Deselect();
            _activeTab = null;
        }
    }
}