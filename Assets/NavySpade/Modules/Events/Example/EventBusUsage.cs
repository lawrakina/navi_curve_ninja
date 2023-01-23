using EventSystem.Runtime.Bus;
using UnityEngine;

namespace Depra.EventSystem.Example
{
    public class EventBusUsage : MonoBehaviour
    {
        public void ExamplePublishMessage()
        {
            EventBus bus = new EventBus();
            //bus.Publish();
        }
    }
}