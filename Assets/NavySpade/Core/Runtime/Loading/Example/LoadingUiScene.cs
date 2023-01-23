using System;
using System.Collections;
using Core.Loading.Operations;
using Main.Levels.Configuration;
using NavySpade.Core.Runtime.App;
using NavySpade.Modules.Configuration.Runtime.SO;


namespace Core.Loading.Example{
    public class LoadingUiScene : ILevelLoadAsyncOperation
    {
        public IEnumerator Invoke()
        {
            LevelsConfig gameConfig = ObjectConfig.GetConfig<LevelsConfig>();
            var loadingSceneOperation = new LoadingScene(gameConfig.UIScene);
            yield return loadingSceneOperation.Invoke();
            SuccessCompleted?.Invoke();
        }

        public float Progress { get; }
        public event Action SuccessCompleted;
    }
}