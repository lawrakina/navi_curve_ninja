using UnityEngine;

namespace Misc.Entities.DynamicObstacles
{
    public abstract class DynamicMovingType
    {
        public Rigidbody Handler { get; private set; }
        public DynamicObstacle Target { get; private set; }

        private bool _isInit;
        
        public void Init(Rigidbody handler, DynamicObstacle executor)
        {
            if(_isInit)
                return;

            Handler = handler;
            Target = executor;
            
            OnInit();

            _isInit = true;
        }
        
        protected virtual void OnInit()
        {
            
        }

        public abstract void Update(float normalValue);

        public virtual void OnDrawGizmos()
        {
            
        }
    }
}