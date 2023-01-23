using System;
using System.Collections;
using Core.Extensions;
using Core.Loading.Operations;
using Main.Levels.Configuration;
using NavySpade.Core.Runtime.Game;
using NavySpade.Modules.Configuration.Runtime.SO;

namespace Core.Loading.Example
{
    public class LoadingGameScene : ILevelLoadAsyncOperation
    {
        public IEnumerator Invoke()
        {
            LevelsConfig gameConfig = ObjectConfig.GetConfig<LevelsConfig>();
            var loadingSceneOperation = new LoadingScene(gameConfig.GameSceneName);
            yield return loadingSceneOperation.Invoke();
            
            GameEnterPoint gameEnterPoint = SceneExtensions.GetRoot<GameEnterPoint>(loadingSceneOperation.LoadedScene);
            gameEnterPoint.Init();
            SuccessCompleted?.Invoke();
        }

        public float Progress { get; }
        public event Action SuccessCompleted;
    }
}