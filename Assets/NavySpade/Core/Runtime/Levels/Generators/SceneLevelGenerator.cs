using System.Collections;
using Core.Extensions;
using Core.Loading;
using Core.Loading.Operations;
using EventSystem.Runtime.Core.Managers;
using Main.Levels.Data;
using Main.Levels.Generators;
using UnityEngine.SceneManagement;

namespace NavySpade.Core.Runtime.Levels.Generators
{
    public class SceneLevelGenerator : LevelGenerator<SceneLevelData>
    {
        private const string LEVEL_NAME = "Level";

        protected override ILevelLoadAsyncOperation OnGenerated(SceneLevelData dataBase)
        {
            IEnumerator LoadLevel()
            {
                if (SceneExtensions.HasSceneBeginningWith(LEVEL_NAME, out var levelScene) == false)
                {
                    var loadingSceneOperation = new LoadingScene(dataBase.BuildIndex);
                    yield return loadingSceneOperation.Invoke();
                    levelScene = loadingSceneOperation.LoadedScene;
                }
                
                SceneManager.SetActiveScene(levelScene);

                LevelBase level = levelScene.GetRoot<LevelBase>();

                level.Init();
                EventManager.Invoke(GameStatesEM.LevelLoaded);
            }

            return new LevelLoadAsyncOperation(LoadLevel());
        }

        protected override ILevelLoadAsyncOperation OnCleanUp()
        {
            IEnumerator UnloadLevel()
            {
                SceneExtensions.HasSceneBeginningWith(LEVEL_NAME, out var levelScene);
                yield return SceneManager.UnloadSceneAsync(levelScene);
            }

            return new LevelLoadAsyncOperation(UnloadLevel());
        }
    }
}