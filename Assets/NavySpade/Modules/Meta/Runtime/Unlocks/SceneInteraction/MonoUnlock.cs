using System;
using UnityEngine;
using Utils.Triggers.Actions;

namespace Core.Meta.Unlocks.SceneInteraction
{
    public class MonoUnlock : MonoBehaviour
    {
        [field: SerializeField] protected bool IsLockedAtStart { get; private set; }
        [SerializeField] private UnlockableCallbacks callbacks;

        private bool IsLockedInternal { get; set; }

        protected void Awake()
        {
            IsLockedInternal = IsLockedAtStart;
            UpdateStatus();
        }

        public virtual void Unlock()
        {
            if (IsLocked() == false)
                return;

            IsLockedInternal = false;
            OnUnlock();
        }

        public virtual void Lock()
        {
            if (IsLocked())
                return;

            IsLockedInternal = true;
            OnLock();
        }

        protected virtual bool IsLocked() => IsLockedInternal;

        protected void OnUnlock()
        {
            callbacks.OnUnlocked();
        }

        protected void OnLock()
        {
            callbacks.OnLocked();
        }

        private void UpdateStatus()
        {
            if (IsLocked())
                OnLock();
            else
                OnUnlock();
        }

        [Serializable]
        private class UnlockableCallbacks
        {
            [SerializeField] private ActionBase _locked;
            [SerializeField] private ActionBase _unlocked;

            public void OnLocked()
            {
                _locked.Fire();
            }

            public void OnUnlocked()
            {
                _unlocked.Fire();
            }
        }
    }
}