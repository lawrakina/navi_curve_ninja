using System;
using UnityEngine;

namespace NavySpade.Modules.Utils
{
    /// <summary>
    /// Like Unity's FixedUpdate, but you can have many of them, and they can all have different tickrates independant from the physics simulation.
    /// </summary>
    public class CustomFixedUpdate
    {
        /// <summary>
        /// FixedUpdateCallback is called every this number of seconds.
        /// </summary>
        public float FixedTimeStep { get; set; }

        /// <summary>
        /// FixedUpdateCallback is called this number of times per second.
        /// </summary>
        public float UpdatesPerSecond
        {
            get => 1.0f / FixedTimeStep;
            set => FixedTimeStep = 1.0f / value;
        }

        /// <summary>
        /// If this value is non-zero, the tickrate can slow down to account for lag.
        /// </summary>
        public float MaxAllowedTimeStep { get; set; }
        
        private Action _fixedUpdateCallback;

        /// <param name="fixedTimeStep"> fixedUpdateCallback is called every this number of seconds </param>
        /// <param name="fixedUpdateCallback"> the callback of the CustomFixedUpdate </param>
        /// <param name="maxAllowedTimestep"> maximum allowed timestep. Set to something other than zero (0.5f is recommended) to prevent a death spiral if the game starts lagging. </param>
        public CustomFixedUpdate(float fixedTimeStep, Action fixedUpdateCallback, float maxAllowedTimestep = 0)
        {
            if (fixedUpdateCallback == null)
            {
                throw new ArgumentException("CustomFixedUpdate needs a valid callback");
            }

            if (fixedTimeStep <= 0f)
            {
                throw new ArgumentException("TimeStep needs to be greater than 0");
            }

            FixedTimeStep = fixedTimeStep;
            _fixedUpdateCallback = fixedUpdateCallback;
            MaxAllowedTimeStep = maxAllowedTimestep;
        }

        /// <param name="updatesPerSecond"> fixedUpdateCallback is called this number of times per second </param>
        /// <param name="fixedUpdateCallback"> the callback of the CustomFixedUpdate </param>
        /// <param name="maxAllowedTimestep"> maximum allowed timestep. Set to something other than zero (0.5f is recommended) to prevent a death spiral if the game starts lagging. </param>
        public CustomFixedUpdate(Action fixedUpdateCallback, float updatesPerSecond, float maxAllowedTimestep)
        {
            if (fixedUpdateCallback == null)
            {
                throw new ArgumentException("CustomFixedUpdate needs a valid callback");
            }

            UpdatesPerSecond = updatesPerSecond;
            _fixedUpdateCallback = fixedUpdateCallback;
            MaxAllowedTimeStep = maxAllowedTimestep;
        }


        /// <summary>
        /// Call this method whenever you want the callback to trigger. It will trigger call FixedUpdateCallback the correct number of times since the previous frame.
        /// </summary>
        public void Update()
        {
            Update(Time.deltaTime);
        }

        /// <summary>
        /// Like the other Update(), but you can specify a custom time since the last update.
        /// </summary>
        private float _timer = 0;

        public void Update(float aDeltaTime)
        {
            _timer -= aDeltaTime;
            if (MaxAllowedTimeStep > 0)
            {
                var timeout = Time.realtimeSinceStartup + MaxAllowedTimeStep;
                while (_timer < 0f && Time.realtimeSinceStartup < timeout)
                {
                    _fixedUpdateCallback();
                    _timer += FixedTimeStep;
                }
            }
            else
            {
                while (_timer < 0f)
                {
                    _fixedUpdateCallback();
                    _timer += FixedTimeStep;
                }
            }
        }
    }
}