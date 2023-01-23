using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;


namespace pj99.Code.Obstacles{
    public class Platform : MonoBehaviour{
        [SerializeField]
        private Transform _startBorder;
        [SerializeField]
        private Transform _endBorder;
        [SerializeField]
        private float _duration = 2f;

        #region PrivateData

        private Vector3 _startPos;
        private Vector3 _endPos;
        private TweenCallback _motor;

        #endregion
        private void Awake(){
            HideBorders();

            transform.DOMove(_endPos, _duration/2).SetEase(Ease.Linear).SetSpeedBased().onComplete += MoveToStart;
        }

        private void MoveToStart(){
            transform.DOMove(_startPos, _duration).SetEase(Ease.Linear).SetSpeedBased().onComplete += MoveToEnd;
        }

        private void MoveToEnd(){
            transform.DOMove(_endPos, _duration).SetEase(Ease.Linear).SetSpeedBased().onComplete += MoveToStart;
        }

        private void HideBorders(){
            if (_startBorder){
                _startPos = _startBorder.position;
                _startBorder.gameObject.SetActive(false);
            }
            if (_endBorder){
                _endPos = _endBorder.position;
                _endBorder.gameObject.SetActive(false);
            }
        }
    }
}