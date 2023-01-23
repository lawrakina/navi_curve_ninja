using System;
using System.Collections.Generic;
using System.Linq;
using Core.Meta;
using EventSystem.Runtime.Core.Managers;
using PathCreation;
using pj99.Code.Extensions;
using pj99.Code.Obstacles;
using UnityEngine;


namespace pj99.Code.Launcher{
    internal class Missile : MonoBehaviour{
        [SerializeField]
        private Animator[] _animator;
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private SliceTester _sliceTester;
        [SerializeField]
        private GameObject _root;
        public event Action<Collider> OnCollision = _ => { };
        public event Action<Missile> OnStop = _ => { };


        #region Animations

        private int START_ANIMATION = Animator.StringToHash("Start");
        private int STOP_ANIMATION = Animator.StringToHash("Stop");

        #endregion


        #region PrivateData

        private VertexPath _path;
        private float _distanceTravelled;
        private float _speed;
        private Vector3 _offsetStay;
        private Collider _other;
        private bool _thowed;

        #endregion


        private void Awake() {
            foreach (var animator in _animator){
                animator.enabled = false;
            }

            _thowed = false;
        }

        public void StartMove(VertexPath path, float speed){
            _path = path;
            _speed = speed;
            _thowed = true;

            foreach (var vector3 in _path.localPoints){
                DebugExtension.DebugPoint(vector3, Color.green, 2f);
            }

            _distanceTravelled = 0;

            foreach (var animator in _animator)
            {
                animator.enabled = true;
                animator.SetTrigger(START_ANIMATION);
            }

            _rigidbody.isKinematic = false;
        }

        private void Stop(){
            ResourceLoader.InstantiateObject(PrefabConfig.Instance.Fx_HitToObstacle, transform.position,
                Quaternion.identity);
            _speed = 0;
            foreach (var animator in _animator){
                animator.enabled = false;
            }

            _path = null;
            _rigidbody.isKinematic = true;
        }

        private void Update(){
            if (_other){
                transform.position = _other.transform.position + _offsetStay;
                return;
            }

            if (_path == null) return;
            if (_speed < 0.1f){
                Stop();
                return;
            }

            CheckDistance();
            RotationAndMoving();
        }

        private void OnTriggerEnter(Collider other){
            OnCollision?.Invoke(other);

            if (other.gameObject.layer == LayerMask.NameToLayer(StringManager.LAYER_OBSTACLE)){
                _offsetStay = transform.position - other.transform.position;
                _other = other;
                OnStop?.Invoke(this);
                EventManager.Invoke(MainEnumEvent.Fail);
                Stop();
            }

            if (other.TryGetComponent(out Enemy enemy)){
                enemy.PreHit();
                _sliceTester.slicer = enemy.Slicer;

                var yPos = new Vector3(0, 1.5f, 0);
                // var yPos = new Vector3(0, CorrectYPos(transform.position.y - enemy.transform.position.y), 0);
                // enemy.Slicer.SliceByMeshPlane(transform.forward, enemy.transform.position + yPos);
                // enemy.Slicer.SliceByMeshPlane(transform.right, enemy.transform.position + yPos);
                enemy.Slicer.SliceByMeshPlane(transform.up, enemy.transform.position + yPos);

                // enemy.Slicer.SliceByMeshPlane(transform.up, enemy.transform.position + new Vector3(0f, transform.position.y - enemy.transform.position.y, 0f));

                enemy.Hit(yPos);

                if (FindObjectsOfType<Launcher>().Any(launcher => !launcher.Pushed))
                {
                    return;
                } 
                _root.SetActive(false);
            }

            if (other.TryGetComponent(out Chain chain)){
                chain.OffJoints();
            }
        }

        private float CorrectYPos(float value){
            if (1 < value && value < 1.6f){
                return 1.5f;
            }

            if (1 > value)
                return 1f;

            //if > 1.6f
            return 1.6f;
        }

        private void CheckDistance(){
            _distanceTravelled += _speed * Time.deltaTime;
        }

        private void RotationAndMoving(){
            var point = _path.GetPointAtDistance(_distanceTravelled, EndOfPathInstruction.Stop);
            var direction = (point - transform.position).normalized;

            var forward = _path.GetDirectionAtDistance(_distanceTravelled, EndOfPathInstruction.Stop);
            var velocity = direction * (_speed * Time.deltaTime);
            velocity = new Vector3(velocity.x, velocity.y, 0f);
            transform.position = velocity + transform.position;

            if (_distanceTravelled >= _path.length){
                _speed = 0;
                return;
            }

            // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward),
            // 400 * Time.deltaTime);
        }
    }
}