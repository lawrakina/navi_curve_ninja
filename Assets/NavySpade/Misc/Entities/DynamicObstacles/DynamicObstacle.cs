using System;
using NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Runtime.Obsolete.SR;
using UnityEngine;

namespace Misc.Entities.DynamicObstacles
{
    [HelpURL("https://docs.google.com/document/d/1pOku7G-X-U1qHPqZLVki4D_UomUsONdF8uoXV8D33Ts/edit#heading=h.82ffvcz9svr3")]
    public class DynamicObstacle : MonoBehaviour
    {
        public enum AutoModeLoopType
        {
            PingPong,
            Repeat
        }
        
        [SR] [SerializeReference] private DynamicMovingType _movingType;

        [Tooltip("скорость 1 = полный цикл за 1 секунду")]
        [SerializeField] private float _speed;

        [Tooltip("будет ли объект двигаться сам или его будет двигать какой-то другой скрипт")]
        [SerializeField] private bool _autoMove = true;
        [Tooltip("Repeat - при достижении конца, объект будет телепортироваться в начало пути \nPing Pong - при достижении конца, объект поедит в противоположную сторону и затем опять вперёд")]
        [SerializeField] private AutoModeLoopType _autoLoopType;

        [field: SerializeField] public bool StartMoveFromThisPosition { get; set; } = true;

        public Rigidbody Handler;

        /// <summary>
        /// скорость 1 = полный цикл за 1 секунду
        /// </summary>
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        /// <summary>
        /// [0-1]
        /// </summary>
        /// <exception cref="Exception"></exception>
        public float Value
        {
            get => _value;
            set
            {
                if (_movingType == null)
                    throw new Exception("тип передвижения не задан");

                _value = Mathf.Clamp01(value);
                ValidateAndFixData();
                _movingType.Update(_value);
            }
        }

        public DynamicMovingType MovingType
        {
            get => _movingType;
            set => _movingType = value;
        }

        public AutoModeLoopType AutoLoopType
        {
            get => _autoLoopType;
            set => _autoLoopType = value;
        }

        private float _value;
        private float _time;

        private void FixedUpdate()
        {
            if (_autoMove)
            {
                if(_autoLoopType == AutoModeLoopType.PingPong)
                    Value = Mathf.PingPong(_time, 1f);
                else if(_autoLoopType == AutoModeLoopType.Repeat)
                {
                    Value = Mathf.Repeat(_time, 1f);
                }
                _time += Time.fixedDeltaTime * Speed;
            }
        }

        private void ValidateAndFixData()
        {
            if (Handler.isKinematic == false)
            {
                Debug.LogWarning("Динамическим препядствиям запрещено иметь физику");
                Handler.isKinematic = true;
            }

            if (_movingType.Handler != Handler)
            {
                _movingType.Init(Handler, this);

                if (StartMoveFromThisPosition)
                {
                    if (_movingType is IInverseValue == false)
                    {
                        Debug.LogWarning("этот тип передвижение не умеет начинать движение с стартовой позиции");
                        return;
                    }

                    _value = (_movingType as IInverseValue).GetValue;
                    _time = _value;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if(_movingType == null || Handler == null)
                return;

            _movingType.Init(Handler, this);
            _movingType.OnDrawGizmos();
        }
    }
}