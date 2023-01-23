using NavySpade.Modules.Configuration.Runtime.SO;
using NavySpade.Modules.Visual.Runtime.Data;
using UnityEngine;

namespace Core.Visual
{
    [CreateAssetMenu(fileName = "VisualConfig", menuName = "Config/Visual", order = 51)]
    public class VisualConfig : ObjectConfig<VisualConfig>
    {
        [SerializeField] private VisualData[] _allVisualVatiantsInGame;
        [SerializeField] private VisualData _selectedFromStart;

        public VisualData[] AllVisualVatiantsInGame => _allVisualVatiantsInGame;

        public VisualData SelectedFromStart => _selectedFromStart;

#if UNITY_EDITOR

        public void SetVisuals(VisualData[] visuals)
        {
            _allVisualVatiantsInGame = visuals;

            if (_selectedFromStart == null && _allVisualVatiantsInGame.Length > 0)
                _selectedFromStart = _allVisualVatiantsInGame[0];
        }

#endif
    }
}