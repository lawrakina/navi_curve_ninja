using System;
using UnityEngine;

namespace Scripts.Shooting
{
    public class DestroyDetector : MonoBehaviour
    {
        public Action OnDestroy;

        private void OnTriggerEnter(Collider other)
        {
            OnDestroy?.Invoke();
        }
    }
}