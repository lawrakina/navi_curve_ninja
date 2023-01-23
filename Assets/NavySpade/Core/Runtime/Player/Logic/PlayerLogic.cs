using Core;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using Main.Levels.Data;
using UnityEngine;

namespace NavySpade.Core.Runtime.Player.Logic
{
    public class PlayerLogic : MonoBehaviour
    {
        private readonly EventDisposal _disposal = new EventDisposal();

        private void Awake()
        {
            EventManager.Add<LevelDataBase>(MainEnumEvent.PreparePlayer, Initialize).AddTo(_disposal);
            EventManager.Add(MainEnumEvent.GeneratePlayer, GeneratePlayer).AddTo(_disposal);
            EventManager.Add(GameStatesEM.OnFail, OnFail).AddTo(_disposal);
        }
        
        private void Initialize(LevelDataBase data)
        {
            EventManager.Invoke(MainEnumEvent.GeneratePlayer);
        }
        
        private void OnFail()
        {
            //CameraManager.Instance.SetTarget(Player.Instance.transform);
        }

        private void GeneratePlayer()
        {
            //this generate player


            //end generate player

            EventManager.Invoke(GenerateEnumEM.SetPlayer, SinglePlayer.Instance);
        }


        private void OnDestroy()
        {
            _disposal.Dispose();
        }
    }
}