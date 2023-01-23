using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainEnumEvent 
{
    Win,
    Fail,
    Pause,
    Clear,
    PrepareLevel,
    PreparePlayer,
    PrepareGame,
    GeneratePlayer,
    SetFinishPoint,
}

public enum GenerateEnumEM
{
    Generate,
    Update,
    SetPlayer,
}

public enum CameraEM
{
    StopShake,
    StartShake,
}
public enum GameStatesEM
{
    Paused,
    EndGame,
    StartGame,
    OnWin,
    OnFail,
    LevelLoaded,
    Restart,
    NextLevel,
}

public enum PopupsEnum
{
    OpenStartGame,
    OpenLosePopup,
    OpenWinPopup,
}
