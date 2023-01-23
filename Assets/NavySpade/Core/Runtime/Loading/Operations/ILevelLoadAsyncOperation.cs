using System;
using System.Collections;

namespace Core.Loading.Operations
{
    public interface ILevelLoadAsyncOperation
    {
        public IEnumerator Invoke();
        public float Progress { get; }
        public event Action SuccessCompleted;
    }
    public class LevelLoadAsyncOperation : ILevelLoadAsyncOperation
    {
        public LevelLoadAsyncOperation(IEnumerator loader, Action successCallback = null)
        {
            _loader = loader;

            if (successCallback != null)
                SuccessCompleted += successCallback;
        }
        
        private IEnumerator _loader;

        public IEnumerator Invoke()
        {
            yield return _loader;
        }

        //todo; impliment
        public float Progress { get; }

        public event Action SuccessCompleted;
    }
}