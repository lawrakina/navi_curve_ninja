using UnityEngine;

namespace NavySpade.Modules.Saving.Runtime.Data
{
    [CreateAssetMenu(fileName = "Transform Data Container", menuName = "Misc/SO/TransformData", order = 51)]
    public class TransformDataContainer : ScriptableObject
    {
        public TransformNode ParentNode;
    }
}