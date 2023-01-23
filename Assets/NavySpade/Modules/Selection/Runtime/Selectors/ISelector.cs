using JetBrains.Annotations;
using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime.Selectors
{
    public interface ISelector
    {
        [PublicAPI]
        void Check(Ray ray);
        
        [PublicAPI]
        Transform GetSelection();
    }
}