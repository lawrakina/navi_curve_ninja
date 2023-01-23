using Core.Loading;
using UnityEngine;

namespace NavySpade.Core.Runtime.Game
{
    public class ProjectContext : MonoBehaviour
    {
        [field: SerializeField] public LoadingScreen LoadingScreen { get; private set; }
        [field: SerializeField] public SplashScreen SplashScreen { get; private set; }

        public static ProjectContext Instance { get; private set; }

        public void Init()
        {
            Instance = this;

            LoadingScreen.Init();
            SplashScreen.Init();
        }
    }
}