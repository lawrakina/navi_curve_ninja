using System;
using UnityEngine;


namespace pj99.Code.Launcher{
    public class CustomAnimEvents : MonoBehaviour{
        public event Action OnPush = () => { };
        public event Action OnCatch = () => { };

        public void Push(){
            OnPush?.Invoke();
        }

        public void Catch(){
            OnCatch?.Invoke();
        }
    }
}