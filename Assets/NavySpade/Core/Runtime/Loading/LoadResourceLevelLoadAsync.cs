using System;
using System.Collections;
using Core.Loading.Operations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Loading
{
    public class LoadResourceLevelLoadAsync<T> : ILevelLoadAsyncOperation where T : Object
    {
        private string _resourcePath;
        
        public T Asset { get; private set; }
        
        public LoadResourceLevelLoadAsync(string path)
        {
            _resourcePath = path;
        }
        
        public IEnumerator Invoke()
        {
            var request = Resources.LoadAsync<T>(_resourcePath);
            yield return request;
            Asset = request.asset as T;
            SuccessCompleted?.Invoke();
        }

        public float Progress { get; }
        public event Action SuccessCompleted;
    }
}