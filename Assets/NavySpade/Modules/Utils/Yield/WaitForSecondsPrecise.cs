using UnityEngine;

namespace NavySpade.Modules.Utils.Yield
{
    /// <summary>
    /// The built-in WaitForSeconds is not precise if you need to use many of them consecutively.
    /// Each WaitForSeconds can be off by up to one frame, and this errors can add up.
    /// </summary>
    /// <remarks>
    /// Create one PreciseSecondsCounter for each routine you use.
    /// </remarks>
    public class WaitForSecondsPrecise : CustomYieldInstruction
    {
        public override bool keepWaiting
            => _counter.TotalWaitTimeMet == false;

        private readonly PreciseSecondsCounter _counter;

        public WaitForSecondsPrecise(PreciseSecondsCounter counter, float seconds)
        {
            _counter = counter;
            counter.AddWait(seconds);
        }
    }
}