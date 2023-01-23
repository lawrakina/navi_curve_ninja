namespace NavySpade.Modules.Utils.Timers
{
    /// <summary>
    /// <a href="https://navyspade.atlassian.net/l/cp/XfavcZzn">Documentation</a>.
    /// </summary>
    public class Timer
    {
        private float _initTime;
        private float _currentTime;

        public Timer(float initTime)
        {
            _initTime = initTime;
            Reload();
        }

        public void ChangeInitTime(float newInitTime)
        {
            _initTime = newInitTime;
            Reload();
        }

        public void Update(float deltaTime)
        {
            if (_currentTime > 0) _currentTime -= deltaTime;
        }

        public void Reload()
        {
            _currentTime = _initTime;
        }

        public bool IsFinish()
        {
            return _currentTime <= 0;
        }
    }
}