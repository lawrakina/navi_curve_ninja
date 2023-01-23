using System;
using UniRx;
using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Utility action that when called will wait a specified time, then fire another action.
    /// Useful for timing things like destroying a crossbow bolt 10 seconds after it's been spawned.
    /// </summary>
    public class DelayedAction : ActionBase
    {
        [Min(0f)] [SerializeField] private float _delay = 1f;
        [SerializeField] private ActionBase _action;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public override void Fire()
        {
            Observable.Timer(TimeSpan.FromSeconds(_delay))
                .Subscribe(_ => _action.Fire())
                .AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}