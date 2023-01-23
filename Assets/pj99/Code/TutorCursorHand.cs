using System;
using UnityEngine;


namespace pj99.Code {
    public class TutorCursorHand : MonoBehaviour {
        [SerializeField] private KeyCode _keyCode = KeyCode.F;
        [SerializeField] private KeyCode _keyCode2 = KeyCode.R;
        [SerializeField] private RectTransform _hand;
        private RectTransform _self;

        private void Awake() {
            _self = GetComponent<RectTransform>();
        }

        private void Update() {
            if (Input.GetKeyDown(_keyCode))
            {
                _hand.gameObject.SetActive(!_hand.gameObject.activeSelf);
            }
            
            if (Input.GetKeyDown(_keyCode2))
            {
                Gui.Instance.RestartButton.gameObject.SetActive(!Gui.Instance.RestartButton.gameObject.activeSelf);
                Gui.Instance.LabelLevel.SetActive(!Gui.Instance.LabelLevel.activeSelf);
            }
            
            

            _hand.anchoredPosition = new Vector2(
                _self.rect.height / Screen.height * Input.mousePosition.x,
                _self.rect.width / Screen.width * Input.mousePosition.y);
        }
    }
}