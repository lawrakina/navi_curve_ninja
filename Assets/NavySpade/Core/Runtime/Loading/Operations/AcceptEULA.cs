using System;
using System.Collections;
using EventSystem.Runtime.Core.Managers;
using UnityEngine;

namespace Core.Loading.Operations
{
    public class AcceptEULA : ILevelLoadAsyncOperation
    {
        private bool _eulaAccepted;

        public IEnumerator Invoke()
        {
            EventManager.Add("eula_close", () =>
            {
                _eulaAccepted = true;
            });
            EventManager.Invoke("eula_open");

            yield return new WaitUntil(() => _eulaAccepted);
            Progress = 1;
            SuccessCompleted?.Invoke();
        }

        public float Progress { get; private set; }
        public event Action SuccessCompleted;
    }
}