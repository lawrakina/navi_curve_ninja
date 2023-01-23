using System;
using System.Collections;

namespace Core.Loading.Operations
{
    public class ActionOperation : ILevelLoadAsyncOperation
    {
        private Action _action;

        public ActionOperation(Action action)
        {
            _action = action;
        }
        
        public IEnumerator Invoke()
        {
            _action.Invoke();
            SuccessCompleted?.Invoke();
            yield break;
        }

        public float Progress { get; }
        public event Action SuccessCompleted;
    }
}