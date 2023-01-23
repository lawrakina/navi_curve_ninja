using UnityEngine;

namespace NavySpade.Modules.StateMachines.Runtime.Mono
{
    public class StateMachineInitialization : MonoBehaviour
    {
        private StateMachineBehaviour _stateMachine;

        private void Awake()
        {
            _stateMachine = GetComponent<StateMachineBehaviour>();
            
            Initialize();
        }

        private void Start()
        {
            _stateMachine.StartMachine();
        }

        /// <summary>
        /// Internally used within the framework to auto start the state machine.
        /// </summary>
        public void Initialize()
        {
            // Turn off all states:
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}