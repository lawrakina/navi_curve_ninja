using Core.Loading.Operations;
using Main.Levels.Data;
using NavySpade.Modules.Extensions.UnityTypes;

namespace Main.Levels.Generators
{
    public abstract class LevelGeneratorBase : ExtendedMonoBehavior<LevelGeneratorBase>
    {
        public abstract ILevelLoadAsyncOperation Generate(LevelDataBase data);
        
        public abstract ILevelLoadAsyncOperation CleanUp();
    }
}