using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime.RayProviders
{
    public interface IRayProvider
    {
        Ray CreateRay();
    }
}