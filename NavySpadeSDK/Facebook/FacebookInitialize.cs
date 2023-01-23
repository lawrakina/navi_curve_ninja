using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class FacebookInitialize : MonoBehaviour
{
    public static FacebookInitialize Instance;
    public static void Initialize()
    {
        if (Instance == null)
        {
        
            var prefab = Resources.Load<FacebookInitialize>("FacebookController");
            var instance = Instantiate(prefab);
            Instance = instance;
            DontDestroyOnLoad(Instance.gameObject);
            try
            {
                Instance.InitializeFb();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    private void InitializeFb()
    {
        try
        {
            if (!FB.IsInitialized)
            {
                // Initialize the Facebook SDK
                FB.Init(OnInitComplete, OnHideUnity);
            }
            else
            {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
        }
        catch (Exception)
        {// ignored
        }
                             
        
    }
    private void OnInitComplete()
    {
        try
        {
            if (FB.IsInitialized) 
            {
                        // Signal an app activation App Event
                FB.ActivateApp();
                        // Continue with Facebook SDK
                        // ...
            } else {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }
        catch (Exception)
        {// ignored
        }
        
    }

    private void OnHideUnity(bool isGameShown)
    {
       /* if (!isGameShown) {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        } else {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }*/
        
    }


    public void FBLogin()
    {
        try
        {
            var perms = new List<string>(){"public_profile", "email"};
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
        catch (Exception)
        {// ignored
        }
        

         
    }
    private void AuthCallback (ILoginResult result) {

        try
        {
            if (FB.IsLoggedIn) {
                // AccessToken class will have session details
                var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
                // Print current access token's User ID
                Debug.Log(aToken.UserId);
                // Print current access token's granted permissions
                foreach (string perm in aToken.Permissions) {
                    Debug.Log(perm);
                }
            } 
            else 
            {
                Debug.Log("User cancelled login");
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
    void OnApplicationPause (bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        try
        {
            if (!pauseStatus) {
                //app resume
                if (FB.IsInitialized) {
                    FB.ActivateApp();
                } else {
                    //Handle FB.Init
                    FB.Init( FB.ActivateApp);
                }
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public static void LevelWin(int level_number)
    {
        try
        {
            if (!FB.IsInitialized)
            {
                FB.Init( () => {
                    FB.ActivateApp();
                    LevelWin(level_number);
                });
                return;
            }

            var tutParams = new Dictionary<string, object>
            {
                {AppEventParameterName.ContentID, "Achieved_Level_" + level_number},
                {AppEventParameterName.ContentType, "Achieved_Level"},
                {AppEventParameterName.Description, "Level comlited. Status - win."},
                {AppEventParameterName.Level, level_number.ToString()},
                {AppEventParameterName.Success, "1"}
            };
            //AchievedLevel
            FB.LogAppEvent (
                AppEventName.AchievedLevel,
                parameters: tutParams
            );
        }
        catch (Exception)
        {
            // ignored
        }
    }
    public static void LevelWin10()
    {
        try
        {
            if (!FB.IsInitialized)
            {
                FB.Init( () => {
                    FB.ActivateApp();
                    LevelWin10();
                });
                return;
            }

            var tutParams = new Dictionary<string, object>
            {
                {AppEventParameterName.ContentID, "Achieved_Level_10"},
                {AppEventParameterName.ContentType, "Achieved_Level_10"},
                {AppEventParameterName.Description, "Level 10 comlited. Status - win."},
                {AppEventParameterName.Level, "10"},
                {AppEventParameterName.Success, "1"}
            };
            //AchievedLevel
            FB.LogAppEvent (
                "fb_mobile_level_achieved_10",
                parameters: tutParams
            );
        }
        catch (Exception)
        {
            // ignored
        }
    }

    /// <summary>
    /// Purchase FB event
    /// </summary>
    /// <param name="count"> gem count</param>
    /// <param name="currency"> ISO-4217 3-letter code for currency used (e.g. "USD", "EUR", "GBP") </param>
    /// <param name="price"> coast </param>
    public static void PurchaseEvent(int count, string currency, string price)
    {

        try
        {
            if (!FB.IsInitialized)
            {
                FB.Init( () => {
                    FB.ActivateApp();
                    PurchaseEvent(count, currency, price);
                });
                return;
            }

            
            var tutParams = new Dictionary<string, object>
            {
                {AppEventParameterName.ContentID, "buy_currency_" + count + "_" + "crystal"},
                {AppEventParameterName.Currency, currency},
                {AppEventParameterName.ContentType, "in_app_purchase"},
                {AppEventParameterName.Description, "InApp purchase in currency store"},
                {AppEventParameterName.NumItems, count},
                {AppEventParameterName.Success, "1"}
            };

            //AchievedLevel
            FB.LogAppEvent (
                AppEventName.Purchased,
                parameters: tutParams
            );
        }
        catch (Exception)
        {
            // ignored
        }
    }
    
    public static void ShowADSCrystal(int count)
    {
        try
        {
            if (!FB.IsInitialized)
            {
                FB.Init( () => {
                    FB.ActivateApp();
                    ShowADSCrystal(count);
                });
                return;
            }

            var tutParams = new Dictionary<string, object>
            {
                {AppEventParameterName.ContentID, "ads_reward_" + count + "_" + "crystal"},
                {AppEventParameterName.Currency, "ADS"},
                {AppEventParameterName.ContentType, "show_ads"},
                {AppEventParameterName.Description, "show ads and get crystal"},
                {AppEventParameterName.NumItems, count},
                {AppEventParameterName.Success, "1"}
            };

            //AchievedLevel
            FB.LogAppEvent (
                AppEventName.Purchased,
                parameters: tutParams
            );
        }
        catch (Exception)
        {
            // ignored
        }
    }
    public static void ShowADSBubble(int count)
    {

        try
        {
            if (!FB.IsInitialized)
            {
                FB.Init( () => {
                    FB.ActivateApp();
                    ShowADSCrystal(count);
                });
                return;
            }

            var tutParams = new Dictionary<string, object>
            {
                {AppEventParameterName.ContentID, "ads_reward_" + count + "_" + "bubbles"},
                {AppEventParameterName.Currency, "ADS"},
                {AppEventParameterName.ContentType, "show_ads"},
                {AppEventParameterName.Description, "show ads and get bubbles"},
                {AppEventParameterName.NumItems, count},
                {AppEventParameterName.Success, "1"}
            };

            //AchievedLevel
            FB.LogAppEvent (
                AppEventName.Purchased,
                parameters: tutParams
            );
        }
        catch (Exception)
        {
            // ignored
        }
    }

    
    public static void LogAppEvent()
    {
        try
        {
            var tutParams = new Dictionary<string, object>
            {
                [AppEventParameterName.ContentID] = "tutorial_step_1",
                [AppEventParameterName.Description] = "First step in the tutorial, clicking the first button!",
                [AppEventParameterName.Success] = "1"
            };

            FB.LogAppEvent (
                AppEventName.Purchased,
                parameters: tutParams
            );
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
