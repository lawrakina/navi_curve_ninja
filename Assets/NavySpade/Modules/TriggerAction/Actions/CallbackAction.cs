using System;

namespace Utils.Triggers.Actions
{
    public class CallbackAction : ActionBase
    {
        private Action _callback;

        public void Init(Action callback)
        {
            _callback = callback;
        }

        public override void Fire()
        {
            _callback.Invoke();
        }
    }
}
