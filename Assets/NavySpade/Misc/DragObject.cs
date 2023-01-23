using System;
using UnityEngine;
using UnityEngine.Events;

namespace Misc
{
    public class DragObject : MonoBehaviour
    {
        [Serializable]
        public class CallbackEvents
        {
            public UnityEvent StartDrag;
            public UnityEvent EndDrag;
        }
        
        [SerializeField] private Vector3 _offset;

        [SerializeField] private Vector3 _boxCastSize;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Rigidbody _movingBody;
        [SerializeField] private RigidbodyConstraints _moveConstraints;

        [SerializeField] private CallbackEvents _events;

        private bool _isDrag;
        private float _startDragDistance;

        private static RaycastHit[] _raycastContainer;

        private void Awake()
        {
            if (_raycastContainer == null)
                _raycastContainer = new RaycastHit[32];
        }

        void OnMouseDown()
        {
            //if (!GameLogic.Instance.IsStarted) todo: isPlay how? 
            //  return;

            _isDrag = true;
            _movingBody.constraints = _moveConstraints;
            _events.StartDrag.Invoke();
        }

        private void Update()
        {
            if (_isDrag == false)
                return;
            
            if (Input.GetMouseButtonUp(0))
            {
                _isDrag = false;
                _movingBody.constraints = RigidbodyConstraints.None;
                
                _events.EndDrag.Invoke();
                return;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            var hitsCount = Physics.BoxCastNonAlloc(
                ray.origin, 
                _boxCastSize / 2f, 
                ray.direction, 
                _raycastContainer,
                Quaternion.identity,
                900, _groundMask);

            var isMove = false;

            for (int i = 0; i < hitsCount; i++)
            {
                var item = _raycastContainer[i];
                if(item.transform == transform)
                    continue;
                
                var point = ray.GetPoint(item.distance);
                transform.position = point + _offset;
                
                    _startDragDistance = item.distance;

                isMove = true;
            }
            
            if(isMove)
                return;

            transform.position = ray.GetPoint(_startDragDistance);
        }
    }
}