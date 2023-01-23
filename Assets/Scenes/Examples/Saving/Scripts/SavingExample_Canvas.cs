using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SavingExample {
    public class SavingExample_Canvas : MonoBehaviour {

        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;

        private void Awake() {
            
            _saveButton.onClick.AddListener(() => {
                foreach (var unit in FindObjectsOfType<SavingExample_Unit>()) {
                    unit.Save();
                }
            });
            
            _loadButton.onClick.AddListener(() => {
                foreach (var unit in FindObjectsOfType<SavingExample_Unit>()) {
                    unit.Load();
                }
            });
        }
    }
}