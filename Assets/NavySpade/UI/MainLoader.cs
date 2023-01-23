using System.Collections;
using System.Collections.Generic;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using Main.Levels.Configuration;
using NavySpade.Core.Runtime.App;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI
{
    public class MainLoader : MonoBehaviour
    {
        public static MainLoader Instance { get; private set; }

        public Image fillBar;
        public List<TextMeshProUGUI> percents = new List<TextMeshProUGUI>();

        private EventDisposal _eventDisposal = new EventDisposal();

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            EventManager.Add("start loading game", StartLoadingGameScene).AddTo(_eventDisposal);
        }

        private void OnDisable()
        {
            _eventDisposal.Dispose();
        }

        public void StartLoadingGameScene()
        {
            StartCoroutine(LoadData(LevelsConfig.Instance.GameSceneName));
        }

        public void SetParameters()
        {
            //this print start game parameters
        }

        private IEnumerator LoadData(string sceneName)
        {
            var loader = SceneManager.LoadSceneAsync(sceneName);

            while (!loader.isDone)
            {
                fillBar.fillAmount = loader.progress;
                foreach (var pp in percents)
                {
                    pp.text = (loader.progress * 100).ToString("F0") + "%";
                }

                yield return null;
            }

            loader.allowSceneActivation = true;
        }
    }
}