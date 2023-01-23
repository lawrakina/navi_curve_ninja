using JetBrains.Annotations;
using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime.Responses
{
    internal interface ISelectionResponse
    {
        [PublicAPI]
        void OnSelect(Transform selection);
        
        [PublicAPI]
        void OnDeselect(Transform selection);
    }
}