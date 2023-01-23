using System.Collections;
using System.Collections.Generic;
using EventSystem.Runtime.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Gui : MonoBehaviour
{
    public static Gui Instance;

    [Header("Components")] 
    [SerializeField] private Canvas canvas;
    [SerializeField]
    private DynamicJoystick _dynamicJoystick;

    [SerializeField]
    private TMP_Text _levelIndex;

    [field:SerializeField]
    public Button RestartButton{ get; set; }
    public int LevelIndex{
        set => _levelIndex.text = $"Level {value}";
    }

    public DynamicJoystick DynamicJoystick{
        get => _dynamicJoystick;
        set => _dynamicJoystick = value;
    }

    private void Awake() {
        Instance = this;
        RestartButton.onClick.AddListener(() => {
            EventManager.Invoke(GameStatesEM.Restart);
        });
    }

    private void OnDestroy() {
        Instance = null;
        RestartButton.onClick.RemoveAllListeners();
    }


    #region getters

    public Canvas Canvas => canvas;
    public GameObject LabelLevel => _levelIndex.gameObject;

    #endregion
}
