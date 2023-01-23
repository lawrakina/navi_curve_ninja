using Core.UI.Popups.Abstract;
using NavySpade.UI.Popups.Abstract;
using TMPro;
using UnityEngine;

namespace Core.UI.Popups
{
    public class ChestPopup : Popup
    {
        //public List<NewChestObject> chests = new List<NewChestObject>();
        [SerializeField] private TMP_Text _keyCountText = default;
        [SerializeField] private TMP_Text _coinsCountText = default;
        [SerializeField] private TMP_Text _addKeysCountText = default;

        [SerializeField] private GameObject _nextButton = default;
        [SerializeField] private GameObject _adsButton = default;

        public double startKeysCount = 0;
        //public GameObject keyO;

        public override void OnAwake()
        {
        }

        public override void OnStart()
        {
            //startKeysCount = CurrencyStorage.KeysCount;
            UpdateCOunter();
            HandleChest();
        }

        public void HandleChest()
        {
            /* foreach (var chest in chests)
             {
                 chest.Initialize(() =>
                 {
                     UpdateCOunter();
                 });
             }*/
        }

        public void UpdateCOunter()
        {
            /* KeyCount.text = Coins.KeysCount.ToString();
             CoinsCount.text = Coins.CoinsCount.ToString();
             AddKeysCountText.text = "+" + MainHandler.Instance.Config.keysCountADS.ToString();
             if (startKeysCount <= 0)
             {
                 if (chests.All(e => e.isOpened))
                 {
                     adsButton.SetActive(false);
                     nextButton.SetActive(true);
                 }
                 else
                 {
                     adsButton.SetActive(true);
                 }
                 nextButton.SetActive(true);
             }
             else
             {
                 adsButton.SetActive(false);
                 nextButton.SetActive(false);
                 if (chests.All(e => e.isOpened))
                 {
                     adsButton.SetActive(false);
                     nextButton.SetActive(true);
                 }
             }*/
        }

        public void ADSAddKey()
        {
            UnityEngine.Debug.Log("Invoke ADS");
            /*GlobalADS.ShowRewardADS(() =>
            {
                Coins.KeysCount += MainHandler.Instance.Config.keysCountADS;
                UpdateCOunter();
            });*/
        }
    }
}