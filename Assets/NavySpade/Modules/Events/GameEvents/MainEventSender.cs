using EventSystem.Runtime.Core.Managers;
using UnityEngine;

namespace EventSystem.GameEvents
{
    public class MainEventSender : MonoBehaviour
    {
        [SerializeField] private MainEnumEvent _event;

        public void Send()
        {
            EventManager.Invoke(_event);
        }
    }
}
