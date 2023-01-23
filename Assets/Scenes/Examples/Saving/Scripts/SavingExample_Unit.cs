using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;
using NavySpade.Modules.Saving.Runtime;
using UnityEditor;
using UnityEngine;

namespace SavingExample {
    public class SavingExample_Unit : MonoBehaviour {
        
        [Header("Components")]
        [SerializeField] private Rigidbody _rb;

        private float _moveSpeed = 3f;

        private Vector3 _moveDirection;
        
        private string POSITION_SAVE_KEY = "SavingExample_Unit_POSITION_SAVE_KEY";
        private string MOVE_DIRECTION_SAVE_KEY = "SavingExample_Unit_MOVE_DIRECTION_SAVE_KEY";
        private string MOVE_SPEED_SAVE_KEY = "SavingExample_Unit_MOVE_SPEED_SAVE_KEY";
        
        #region getters

        public float MoveSpeed {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        #endregion

        private void Awake() {
            
            var strId = transform.GetSiblingIndex();
            
            POSITION_SAVE_KEY += strId;
            MOVE_DIRECTION_SAVE_KEY += strId;
            MOVE_SPEED_SAVE_KEY += strId;

            _moveDirection = transform.right;

            _moveSpeed = UnityEngine.Random.Range(_moveSpeed - 1.5f, _moveSpeed + 1.5f);

            Load();
        }

        private void FixedUpdate() {
            _rb.velocity = _moveDirection * _moveSpeed;
        }

        private void OnCollisionEnter(Collision other) {
            switch (other.collider.gameObject.tag) {
                case "Floor":

                    SwapMoveDirection();

                    break;
            }
        }

        private void SwapMoveDirection() {
            if (_moveDirection == transform.right) {
                _moveDirection = transform.right * -1;
            } else {
                _moveDirection = transform.right;
            }
        }

        public void Save() {
            SaveManager.Save(POSITION_SAVE_KEY, transform.position);
            SaveManager.Save(MOVE_SPEED_SAVE_KEY, _moveSpeed);
            SaveManager.Save(MOVE_DIRECTION_SAVE_KEY, _moveDirection);
        }
        
        public void Load() {
           
            if (SaveManager.HasKey(POSITION_SAVE_KEY) == false) {
                return;
            }

            transform.position = SaveManager.Load<Vector3>(POSITION_SAVE_KEY);
            _moveSpeed = SaveManager.Load<float>(MOVE_SPEED_SAVE_KEY);
            _moveDirection = SaveManager.Load<Vector3>(MOVE_DIRECTION_SAVE_KEY);
        }
    }
}