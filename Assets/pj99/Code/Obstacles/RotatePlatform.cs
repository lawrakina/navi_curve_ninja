using System;
using UnityEngine;


namespace pj99.Code.Obstacles{
    public class RotatePlatform : MonoBehaviour{
        [SerializeField]
        private float _angleInSecond = 100f;
        [SerializeField]
        private RotateAxis _rotateAxis = RotateAxis.Right;

        private void FixedUpdate(){
            switch (_rotateAxis){
                case RotateAxis.Up:
                    transform.Rotate(Vector3.up, Time.fixedDeltaTime * _angleInSecond);
                    break;
                case RotateAxis.Right:
                    transform.Rotate(Vector3.right, Time.fixedDeltaTime * _angleInSecond);
                    break;
                case RotateAxis.Forward:
                    transform.Rotate(Vector3.forward, Time.fixedDeltaTime * _angleInSecond);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    internal enum RotateAxis{
        Up, Right, Forward
    }
}