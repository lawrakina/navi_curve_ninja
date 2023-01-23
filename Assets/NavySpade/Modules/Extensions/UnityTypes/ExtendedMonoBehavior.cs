using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace NavySpade.Modules.Extensions.UnityTypes
{
    public class ExtendedMonoBehavior : MonoBehaviour
    {
        #region Coroutines

        /// <summary>
        /// Invoke at Coroutine.
        /// </summary>
        /// <param name="time">in seconds</param>
        /// <param name="endAction"> Action </param>
        public Coroutine InvokeAtTime(float time, Action endAction)
        {
            return StartCoroutine(WaitPlugin(time, endAction));
        }

        /// <summary>
        /// Wait condition.
        /// </summary>
        /// <param name="cond">Condition. If true then endAction invoke</param>
        /// <param name="endAction"> end action</param>
        public Coroutine InvokeAtCondition([NotNull] Func<bool> cond, Action endAction)
        {
            return StartCoroutine(WaitConditionPlugin(cond, endAction));
        }

        private IEnumerator WaitPlugin(float time, Action endAction)
        {
            yield return new WaitForSeconds(time);
            endAction?.Invoke();
        }

        private IEnumerator WaitConditionPlugin(Func<bool> cond, Action endAction)
        {
            while (cond.Invoke() == false)
            {
                yield return null;
            }

            endAction?.Invoke();
            yield return null;
        }
        
        #endregion

        #region Syntax
        
        public void GetOrAddComponent<T>() where T: Component
        {
            gameObject.GetOrAddComponent<T>();
        }
        
        public bool TryGetOrAddComponent<T>(out T component) where T: Component
        {
            component = gameObject.GetOrAddComponent<T>();

            return component != null;
        }
        
        #endregion
    }

    /// <summary>
    /// ecs?
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtendedMonoBehavior<T> : ExtendedMonoBehavior where T : ExtendedMonoBehavior<T>
    {
        #region ecs

        public static T Instance => All.First();

        public static HashSet<T> All { get; private set; } = new HashSet<T>();
        public static HashSet<T> Active { get; private set; } = new HashSet<T>();

        protected virtual void Awake()
        {
            if (All == null)
                All = new HashSet<T>();

            All.Add((T) this);
        }

        protected virtual void OnEnable()
        {
            if (Active == null)
                Active = new HashSet<T>();
            
            Active.Add((T) this);
        }

        protected virtual void OnDisable()
        {
            Active.Remove((T) this);
        }

        protected virtual void OnDestroy()
        {
            All.Remove((T) this);
        }

        #endregion
    }
}
