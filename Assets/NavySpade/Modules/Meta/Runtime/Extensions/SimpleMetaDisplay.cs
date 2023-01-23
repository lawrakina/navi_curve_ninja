using UnityEngine;

namespace NavySpade.Meta.Runtime.Extensions
{
    [CreateAssetMenu(fileName = "New Meta Display", menuName = "Meta/Extensions/Simple Display", order = 0)]
    public class SimpleMetaDisplay : MetaExtensionBase
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Sprite Background { get; private set; }
    }
}
