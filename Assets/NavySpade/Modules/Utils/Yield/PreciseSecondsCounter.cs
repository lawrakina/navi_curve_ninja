using UnityEngine;

namespace NavySpade.Modules.Utils.Yield
{
    public abstract class PreciseSecondsCounter
    {
        protected abstract float GetCurrentTime();

        private readonly float _timeOnCreation;

        public PreciseSecondsCounter()
        {
            _timeOnCreation = GetCurrentTime();
        }

        private float _totalWaitTime = 0;

        internal void AddWait(float seconds)
            => _totalWaitTime += seconds;

        internal bool TotalWaitTimeMet
            => GetCurrentTime() >= _timeOnCreation + _totalWaitTime;
    }

    /// <summary>
    /// Measures in scaled time (<see cref="Time.time"/>)
    /// </summary>
    public class PreciseSecondsCounterScaled : PreciseSecondsCounter
    {
        protected override float GetCurrentTime()
            => Time.time;
    }

    /// <summary>
    /// Measures in unscaled time (<see cref="Time.unscaledTime"/>)
    /// </summary>
    public class PreciseSecondsCounterUnscaled : PreciseSecondsCounter
    {
        protected override float GetCurrentTime()
            => Time.unscaledTime;
    }
}