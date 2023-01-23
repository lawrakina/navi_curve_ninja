using System.Collections;
using Core.Loading.Operations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _slider;
        [SerializeField] private string _sliderTextFormat = "Loading {0}%...";
        [SerializeField] private TMP_Text _sliderText;
        

        private OperationExecutor _operationExecutor;
        
        public void Init()
        {
            _operationExecutor = new OperationExecutor();
            
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        
        public void Execute(params ILevelLoadAsyncOperation[] asyncOperation)
        {
            StartCoroutine(Executing(asyncOperation));
        }

        private IEnumerator Executing(ILevelLoadAsyncOperation[] asyncOperation)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            SetValue(0);

            int completedOperations = 0;
            foreach (var operation in asyncOperation)
            {
                yield return _operationExecutor.Execute(operation);
                completedOperations++;
                SetValue((float) completedOperations / asyncOperation.Length);
            }
            
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }
        
        private void SetValue(float value)
        {
            value = Mathf.Clamp01(value);
            var percentValue = (int)(value * 100);
            
            _slider.value = value;
            _sliderText.text = string.Format(_sliderTextFormat, percentValue);
        }
    }
}