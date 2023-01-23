using System;
using System.Collections;

namespace Core.Loading.Operations
{
    public class FuncOperation : ILevelLoadAsyncOperation
    {
        private Func<IEnumerator> _func;
        
        public FuncOperation(Func<IEnumerator> func)
        {
            _func = func;
        }
        
        public IEnumerator Invoke()
        {
            yield return _func.Invoke();
            SuccessCompleted?.Invoke();
        }

        public float Progress { get; }
        public event Action SuccessCompleted;
    }
}