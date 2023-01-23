using AYellowpaper;
using EventSystem.Integrations.Toolkit.SO.Structs;
using EventSystem.Runtime.Core.Registration;
using EventSystem.Runtime.Core.Registration.Senders;
using NaughtyAttributes;
using UnityEngine;

namespace NavySpade.Modules.Events.Integrations.Toolkit.Registration.Senders
{
    public class VoidSender : MonoBehaviour, IEventSender
    {
        [SerializeField] private InterfaceReference<IRegisteredEvent<Void>> _event;

        [Button]
        public void Send()
        {
            _event?.Value?.Invoke(new Void());
        }
    }
}