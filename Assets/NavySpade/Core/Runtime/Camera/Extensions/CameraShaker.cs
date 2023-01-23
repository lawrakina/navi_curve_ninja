using System.Collections;
using Cinemachine;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using NaughtyAttributes;
using UnityEngine;

namespace NavySpade.Misc.Cinemachine
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private bool _shakingUntilStoped;
        [SerializeField, ShowIf("_shakingUntilStoped")] private float _amplitude;
        [SerializeField, HideIf("_shakingUntilStoped")] private AnimationCurve _amplitudeByRealTime;
        
        private CinemachineVirtualCamera _camera;
        private CinemachineBasicMultiChannelPerlin _noise;

        private Coroutine _shakeRoutine;
        private EventDisposal _eventDisposal;
        
        private void Awake()
        {
            _eventDisposal = new EventDisposal();
            _camera = GetComponent<CinemachineVirtualCamera>();
            _noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            
            EventManager.Add(GameStatesEM.EndGame, StopShake).AddTo(_eventDisposal);
            EventManager.Add(CameraEM.StopShake, StopShake).AddTo(_eventDisposal);
            EventManager.Add(CameraEM.StartShake, StartShake).AddTo(_eventDisposal);
        }
        
        private void OnDestroy()
        {
            _eventDisposal.Dispose();
        }

        [Button("Start Shake")]
        public void StartShake()
        {
            if (_shakeRoutine != null)
            {
                _noise.m_AmplitudeGain = 0f;
                StopCoroutine(_shakeRoutine);
            }
            
            _shakeRoutine = StartCoroutine(Shake());
        }

        private void StopShake()
        {
            if (_shakeRoutine == null)
            {
                return;
            }

            _noise.m_AmplitudeGain = 0;
            StopCoroutine(_shakeRoutine);
            _shakeRoutine = null;
        }

        private IEnumerator Shake()
        {
            if (_shakingUntilStoped)
            {
                _noise.m_AmplitudeGain = _amplitude;
                yield break;
            }

            var maxTime = _amplitudeByRealTime.keys[_amplitudeByRealTime.length - 1].time;
            var currentTime = 0f;
            
            while (currentTime < maxTime)
            {
                yield return null;

                _noise.m_AmplitudeGain = _amplitudeByRealTime.Evaluate(currentTime);
                currentTime += Time.unscaledDeltaTime;
            }

            _noise.m_AmplitudeGain = 0f;
            _shakeRoutine = null;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}