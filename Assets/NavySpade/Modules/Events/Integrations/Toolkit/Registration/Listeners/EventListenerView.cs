using EventSystem.Runtime.Core.Registration.Containers;
using EventSystem.Runtime.Core.Registration.Listeners;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace EventSystem.Integrations.Toolkit.Registration.Listeners
{
    public class EventListenerView : MonoBehaviour, IEventListener
    {
        [field: SR]
        [field: SerializeReference]
        public IEventListenerContainer[] Events { get; private set; }

        public void Subscribe()
        {
            foreach (var eventData in Events)
            {
                eventData.Subscribe();
            }
        }

        public void Unsubscribe()
        {
            foreach (var eventData in Events)
            {
                eventData.Unsubscribe();
            }
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }
    }
}