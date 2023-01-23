using System;
using Core.Loading.Operations;
using Main.Levels.Data;

namespace Main.Levels.Generators
{
    public abstract class LevelGenerator<T> : LevelGeneratorBase where T : LevelDataBase
    {
        protected T CurrentLevelData { get; private set; }
        
        private LevelDataBase.AdditionData[] _datas;

        public override ILevelLoadAsyncOperation Generate(LevelDataBase dataBase)
        {
            if (dataBase is T == false)
            {
                throw new Exception("этот уровень имеет другой тип");
            }

            CurrentLevelData = dataBase as T;
            
            var operation = OnGenerated(CurrentLevelData);

            operation.SuccessCompleted += InvokeAdditionsParams;

            return operation;
        }

        private void InvokeAdditionsParams()
        {
            if (_datas == null)
            {
                _datas = CurrentLevelData.AdditionsData;
                
                foreach (var data in _datas)
                {
                    data.ExtensionData.Apply();
                }
            }
            else
            {
                foreach (var data in _datas)
                {
                    data.ExtensionData.Clear();
                }

                _datas = CurrentLevelData.AdditionsData;

                foreach (var data in _datas)
                {
                    data.ExtensionData.Apply();
                }
            }
        }

        public override ILevelLoadAsyncOperation CleanUp()
        {
            return OnCleanUp();
        }

        protected abstract ILevelLoadAsyncOperation OnGenerated(T dataBase);
        
        protected abstract ILevelLoadAsyncOperation OnCleanUp();
    }
}