using System;
using UnityEngine;

namespace Misc.RootProviders.Runtime.Base
{
    public abstract class RootProvider : IRootProvider
    {
        public abstract bool IsActive { get; }
        
        public abstract GameObject Root { get; }

        public void Show(Action onComplete)
        {
            ShowOverride(() => OnEnabled(onComplete));
        }

        public void Hide(Action onComplete)
        {
            HideOverride(() => HideInstantly(onComplete));
        }

        protected abstract void ShowOverride(Action onComplete);
        protected abstract void HideOverride(Action onComplete);

        #region Safe access to animation

        public void TryShow() => TryShow(null);

        public void TryShow(Action onComplete)
        {
            if (IsActive == false)
            {
                Show(onComplete);
            }
        }

        public void TryHide() => TryHide(null);

        public void TryHide(Action onComplete)
        {
            if (IsActive)
            {
                Hide(onComplete);
            }
        }

        #endregion

        #region Instant access

        public void HideInstantly() => HideInstantly(null);

        public void HideInstantly(Action onComplete)
        {
            Root.SetActive(false);
            OnDisabled(onComplete);
        }

        public void ShowInstantly()
        {
            Root.SetActive(true);
            OnEnabled();
        }

        #endregion

        protected virtual void OnEnabled(Action onComplete = null)
        {
            onComplete?.Invoke();
        }

        protected virtual void OnDisabled(Action onComplete = null)
        {
            onComplete?.Invoke();
        }

        public virtual void Reset(GameObject parent)
        {
        }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}