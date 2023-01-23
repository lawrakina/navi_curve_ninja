using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Freezes the game for a few frames when this action is hit.
    /// Useful for giving a feeling of weight to player hits.
    /// </summary>
    public class HitPauseAction : ActionBase
    {
        [Min(0)] [SerializeField] private int _framesToPause = 5;
    
        private int _counter;

        public override void Fire()
        {
            Time.timeScale = 0.0f;
            _counter = _framesToPause;
        }

        private void Update()
        {
            _counter--;
            
            if (_counter == 0)
                Time.timeScale = 1.0f;
        }
    }
}