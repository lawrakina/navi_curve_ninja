using System.Collections;
using Core.Loading.Operations;
using UnityEngine;

namespace Core.Loading
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _speed;
        
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
            yield return ShowSplash(true);
            yield return _operationExecutor.Execute(asyncOperation);
            yield return ShowSplash(false);
        }
        
        private IEnumerator ShowSplash(bool show)
        {
            _canvasGroup.interactable = show;
            _canvasGroup.blocksRaycasts = show;
        
            float progress = 0;
            while (progress < 1)
            {
                progress += Time.deltaTime * _speed;
                if (show)
                {
                    _canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
                }
                else
                {
                    _canvasGroup.alpha = Mathf.Lerp(1, 0, progress);
                }
                yield return null;
            }
        }
    }
}