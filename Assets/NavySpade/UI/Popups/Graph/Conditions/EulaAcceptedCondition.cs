using System;
using NavySpade.Modules.Saving.Runtime;

namespace Core.UI.Popups.Graph.Conditions
{
    [Serializable]
    [CustomSerializeReferenceName("Eula Accepted")]
    public class EulaAcceptedCondition : ICondition
    {
        public bool Check()
        {
            return SaveManager.Load<int>("LicenseAccept") != 0;
        }
    }
}