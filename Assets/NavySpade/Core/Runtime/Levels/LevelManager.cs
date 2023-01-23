using System;
using System.Collections;
using System.Linq;
using Core.Loading.Operations;
using Main.Levels.Configuration;
using Main.Levels.Data;
using Main.Levels.Generators;
using NaughtyAttributes;
using NavySpade.Core.Runtime.Game;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace NavySpade.Core.Runtime.Levels
{
    public static class LevelManager
    {
        private const string LevelIndexSaveKey = "LevelIndexSaveKey";
        
        private static LevelsConfig _levelsConfig => LevelsConfig.Instance;
        
        private static int? _levelIndex;
        private static bool _isNeedCleanup;

        public static int LevelIndex
        {
            get
            {
                _levelIndex ??= SaveManager.Load<int>(LevelIndexSaveKey);
                return (int)_levelIndex;
            }
            set
            {
                _levelIndex = value;
                LevelIndexChanged?.Invoke(value);
                SaveManager.Save(LevelIndexSaveKey, value);
            }
        }
        
        public static int LastOpenedLevelIndex
        {
            get => SaveManager.Load<int>("LastOpenedLevelIndex");
            set => SaveManager.Save("LastOpenedLevelIndex", value);
        }
        
        public static bool IsTutorialLevel => LevelIndex < _levelsConfig.Tutorial.Length;
        
        public static LevelBase CurrentLevel { get; set; }
        
        public static event Action<int> LevelIndexChanged;
        public static event Action LevelBeginLoad;
        public static event Action LevelLoaded;
        public static bool IsLoading { get; private set; }

        [Button]
        public static void LoadPreviousLevel()
        {
            LevelIndex = ClampLevelIndex(LevelIndex - 1);
            LoadLevel(LevelIndex);
        }

        [Button]
        public static void RestartLevel()
        {
            LoadLevel(LevelIndex);
        }
        
        public static void UnlockNextLevel()
        {
            LevelIndex = ClampLevelIndex(LevelIndex + 1);
        }
        
        [Button]
        public static void LoadNextLevel()
        {
            LevelIndex = ClampLevelIndex(LevelIndex + 1);
            LoadLevel(LevelIndex);
        }

        private static void LoadLevel(int levelIndex)
        {
            if (IsLoading)
                throw new Exception("level still loading");
            
            var levelDataBase = GetLevelData(levelIndex);
            var levelGenerator = LevelGeneratorBase.Active.FirstOrDefault();

            if (levelGenerator == default)
            {
                throw new NullReferenceException("no one level generators on scene");
            }

            if (LevelGeneratorBase.Active.Count > 1)
            {
                //todo: гарантировать синглотон генератора уровня
                Debug.LogWarning("only one level generator using, other was ignored");
            }

            IEnumerator Loading()
            {
                if (_isNeedCleanup)
                {
                    yield return levelGenerator.CleanUp().Invoke();
                }
                
                _isNeedCleanup = true;

                yield return levelGenerator.Generate(levelDataBase).Invoke();
            }

            var asyncOperation = new LevelLoadAsyncOperation(Loading(), () =>
            {
                IsLoading = false;
                LevelLoaded?.Invoke();
            });

            asyncOperation.SuccessCompleted += () =>
            {
                IsLoading = false;
                LevelLoaded?.Invoke();
            };
            
            ProjectContext.Instance.SplashScreen.Execute(asyncOperation);
        }

        private static int ClampLevelIndex(int levelIndex)
        {
            levelIndex = Mathf.Max(levelIndex, 0);
            int realLevelIndex = levelIndex - _levelsConfig.Tutorial.Length;
            LastOpenedLevelIndex = Mathf.Max(LastOpenedLevelIndex, levelIndex);
            
            if (realLevelIndex >= _levelsConfig.Main.Length)
            {
                levelIndex = _levelsConfig.Tutorial.Length;
            }

            return levelIndex;
        }
        
        private static LevelDataBase GetLevelData(int levelIndex)
        {
            if (levelIndex < 0)
                throw new ArithmeticException();

            if (levelIndex < _levelsConfig.Tutorial.Length)
                return _levelsConfig.Tutorial[levelIndex];
            
            var deltaLevel = levelIndex - _levelsConfig.Tutorial.Length;
            deltaLevel = (int)Mathf.Repeat(deltaLevel, _levelsConfig.Main.Length);
            return _levelsConfig.Main[deltaLevel];
        }
    }
}