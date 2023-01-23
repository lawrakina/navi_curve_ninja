using System.Collections;
using UnityEngine;

namespace Core.UI.Popups.Graph.States
{
    [CreateNodeMenu("Core/Timer")]
    public class WaitTime : State
    {
        public float Second;

        [Input] public StateTransition StartTimer;
        [Output] public StateTransition OnTimeout;
        
        public override void Run()
        {
            RuntimeDispatcher.Instance.StartCoroutine(Await());
        }

        private IEnumerator Await()
        {
            yield return new WaitForSecondsRealtime(Second);
            Complete(GetOutputPort(nameof(OnTimeout)));
        }
    }
}