using System;
using UniRx;
using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Utility action that will call another action,
    /// but not fire again (no matter how many times it is called) until an amount of time has passed.
    /// Super useful for controlling player attacks.
    /// </summary>
    public class CooldownAction : ActionBase
    {
        [field: SerializeField] public float Delay { get; private set; }
        [SerializeField] private ActionBase _action;
        [field: SerializeField] public bool IsInCooldown { get; private set; }

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public override void Fire()
        {
            if (IsInCooldown)
                return;

            _action.Fire();
            IsInCooldown = true;

            Observable.Timer(TimeSpan.FromSeconds(Delay))
                .Subscribe(_ => ClearCooldown())
                .AddTo(_disposable);
        }

        private void ClearCooldown()
        {
            IsInCooldown = false;
        }
    }
}