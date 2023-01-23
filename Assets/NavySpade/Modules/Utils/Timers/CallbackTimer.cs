using System;

namespace NavySpade.Modules.Utils.Timers
{
    public class CallbackTimer
    {
        public float ElapsedTime;

        public float Interval { get; }
        public bool IsStarted { get; private set; }

        private readonly Action _callback;

        public CallbackTimer(float interval, Action callback)
        {
            Interval = interval;
            _callback = callback;
        }

        public void Update(float value)
        {
            if (IsStarted == false)
            {
                return;
            }

            ElapsedTime += value;
            if (ElapsedTime < Interval)
            {
                return;
            }

            _callback?.Invoke();
            Reset();
        }

        public void Start()
        {
            Unpause();
        }

        public void Stop()
        {
            Pause();
            Reset();
        }

        public void Pause()
        {
            IsStarted = false;
        }

        public void Unpause()
        {
            IsStarted = true;
        }

        public void Reset()
        {
            ElapsedTime = 0f;
        }
    }
}