using TMPro;
using UnityEngine;

namespace Core.UI.Counters
{
    public class FallingText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _stateName = "text_fade";

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetColor(Color color)
        {
            _text.color = color;
        }

        public void FadeOut()
        {
            _animator.Play(_stateName, 0, 0);
        }
    }
}
