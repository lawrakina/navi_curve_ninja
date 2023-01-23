using System;
using System.Collections;
using System.Collections.Generic;
using AnalyticsEnums_;
using UnityEngine;

public static class AnalyticsEnums 
{
    public static string GetLevelKeyAtInfo()
    {
        return "N";
        /*switch (info.Levelkey)
        {
            case SortedLevelInfoType.mainMap_:
                return "N";
            case SortedLevelInfoType.dung_:
                return "Q-Z";
            case SortedLevelInfoType.preDunge_:
                return "QS-Z";
            default:
                throw new ArgumentOutOfRangeException();
        }*/
    }
    public static string GetLevelTypeKey(LevelType type_)
    {
        switch (type_)
        {
            case LevelType.Normal:
                return "N";
            case LevelType.QuestZ:
                return "Q-Z";
            case LevelType.SpecialQuestZ:
                return "QS-Z";
            default:
                return "";
        }
    }

    public  static  int GetActualLevelNumber
    {
        get
        {
            return GlobalParameters.LevelNumber;//PlayerPrefs.GetInt("next_level", 1);
        }
    }

    public static string GetScreenCode(ScreenType type_)
    {
        return ((ScreenCode) ((int) type_)).ToString();
    }

    
}

namespace AnalyticsEnums_
{
        

    public enum LevelType
    {
        Normal,
        QuestZ,
        SpecialQuestZ
    }

    public enum PopupEventType
    {
        None,
        Open,
        Close,
        All
    }
    
    public enum ScreenType
    {
        GameScreen, //1
        LevelScreen, //2
        QuestScreen, //3
        AlertPopup,//1
        BuyBoosterPopup_Enlarge,//2
        BuyBoosterPopup_Choose3,//3
        BuyBoosterPopup_Rainbow,//4
        BuyBoosterPopup_Super,//5
        BuyBoosterPopup_Ease,//6
        BuyCoinsPopup,//7
        ChargeBoosterPopup_Cone,//8
        ChargeBoosterPopup_Horizontal,//9
        ConfirmationPopup_exitGame,//10
        ConfirmationPopup_exitLevel,//11
        ConfirmationPopup_positive_ActivateBooster,//12
        DayRewardPopup,//13
        DevTeamPopup,//14
        ExitWithLoseLife,//15
        LicensePopup,//16
        LosePopup,//17
        NotifPopup,//18
        OutOfBubblePopup,//19
        PausePopup,//20
        RewardPopup,//21
        SettingsPopup,//22
        StartGamePopup,//23
        WatchADSPopup,//24
        WinPopup,//25
        Shop_BoosterPage,//26
        Shop_DressPage,//27
        Shop_SpecialOfferPage,//28
        BuyLivesPopup,//29
        FortuneWindow,//30
        ConfirmationPopup_DayReward_change,//31
        Default_Empty,
    }

    public enum ScreenCode
    {
        M01,//1
        M02,//2
        M03,//3
        P01,//1
        P02,//2
        P03,//3
        P04,//4
        P05,//5
        P06,//6
        P07,//7
        P08,//8
        P09,//9
        P10,//10
        P11,//11
        P12,//12
        P13,//13
        P14,//14
        P15,//15
        P16,//16
        P17,//17
        P18,//18
        P19,//19
        P20,//20
        P21,//21
        P22,//22
        P23,//23
        P24,//24
        P25,//25
        P26,//26
        P27,//27
        P28,//28
        P29,
        P30,
        P31,
        UA,
    }

}