using System;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using UnityEngine;

namespace NavySpade.Core.Runtime.Game
{
    public class GameStateHandler : MonoBehaviour
    {
        public bool IsStarted { get; private set; }
        public bool IsPaused { get; private set; }

        private readonly EventDisposal _dispose = new EventDisposal();

        public event Action Started;
        public event Action Ended;

        private void Awake()
        {
            EventManager.Add(GameStatesEM.StartGame, OnGameStarted).AddTo(_dispose);
            EventManager.Add(GameStatesEM.EndGame, OnGameEnded).AddTo(_dispose);
        }

        private void OnGameStarted()
        {
            IsStarted = true;
            Started?.Invoke();
        }

        private void OnGameEnded()
        {
            IsStarted = false;
            Ended?.Invoke();
        }

        public void SetPause(bool isActive)
        {
            IsPaused = !IsPaused;
        }

        private void OnDestroy()
        {
            _dispose.Dispose();
        }
    }
}