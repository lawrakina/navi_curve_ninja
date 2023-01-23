using Depra.EventSystem.Integrations.Toolkit.SO.UnityEvents;
using EventSystem.Integrations.Toolkit.Registration.Listeners;
using EventSystem.Integrations.Toolkit.SO.Structs;
using EventSystem.Runtime.Core.Registration;
using UnityEngine;

namespace Depra.EventSystem.Integrations.Toolkit.Registration.Listeners
{
    public class VoidListenerView : GenericEventListenerView<Void, IRegisteredEvent<Void>, UnityVoidEvent>
    {
    }
}