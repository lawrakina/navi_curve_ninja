using System.Collections;
using Core.Loading;
using Core.Loading.Operations;
using Main.Levels.Data;
using Main.Levels.Generators;

namespace NavySpade.Core.Runtime.Levels.Generators
{
    public class PrefabLevelGenerator : LevelGenerator<ResourceNameLevelData>
    {
        private const string ASSET_DIR = "Levels/";
        
        private LevelBase _currentLevel;

        protected override ILevelLoadAsyncOperation OnGenerated(ResourceNameLevelData dataBase)
        {
            IEnumerator Load()
            {
                var request = new LoadResourceLevelLoadAsync<LevelBase>(ASSET_DIR + dataBase.PrefabName);
                yield return request.Invoke();
            
                _currentLevel = Instantiate(request.Asset);
                _currentLevel.Init();
            }
            
            return new LevelLoadAsyncOperation(Load());
        }

        protected override ILevelLoadAsyncOperation OnCleanUp()
        {
            IEnumerator Unload()
            {
                Destroy(_currentLevel.gameObject);

                yield break;
            }

            return new LevelLoadAsyncOperation(Unload());
        }
    }
}