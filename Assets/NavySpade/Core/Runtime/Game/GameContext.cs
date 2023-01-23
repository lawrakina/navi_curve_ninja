using Core.Game;
using UnityEngine;


namespace NavySpade.Core.Runtime.Game{
    public class GameContext : MonoBehaviour
    {
        public static GameContext Instance { get; private set; }
        
        //[field: SerializeField] public ParticleManager ParticleManager { get; private set; }
        
        [field: SerializeField] public InGameEarnedCurrency EarnedCurrency { get; private set; }
        
        [field: SerializeField] public GameStatesManager GameStatesManager { get; private set; }
        
        public void Init()
        {
            Instance = this;
            
            //ParticleManager.Init();
            GameStatesManager.Init(EarnedCurrency);
        }
    }
}