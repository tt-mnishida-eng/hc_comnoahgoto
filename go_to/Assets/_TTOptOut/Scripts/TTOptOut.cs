// #define USE_MAX
// #define USE_IRONSOURCE

#define USE_TENJIN
// #define USE_ADMOB
#define USE_GAMEANALYTICS
//#define USE_FIREBASE
#define USE_FACEBOOK
#define USE_UNITYANALYTICS
// #define USE_ADJUST

using System.Collections.Generic;
using UnityEngine;

namespace TT.OptOut
{
    public static class TTOptOut
    {
        public static bool NeedOptOut()
        {
            return PlayerPrefs.GetInt(TTOptOutPlayerPrefKeys.IsOptOut, 0) == 1;
        }

        public static void SetNeedOptOut(bool flg)
        {
            PlayerPrefs.SetInt(TTOptOutPlayerPrefKeys.IsOptOut, flg ? 1 : 0);
            PlayerPrefs.Save();
            if (flg)
            {
                DoOptOut();
            }
            else
            {
                DoOptIn();
            }
        }

        public static void DoOptInOrOut()
        {
            if (NeedOptOut())
            {
                DoOptOut();
            }
            else
            {
                DoOptIn();
            }
        }

        public static void DoOptOut()
        {
            Debug.Log("### TTOptOut : DoOptOut");
            MaxOptOut();
            IronSourceOptOut();
            AdMobOptOut();
            TenjinOptOut();
            GameAnalyticsOptOut();
            FirebaseOptOut();
            FacebookOptOut();
            UnityAnalyticsOptOut();
            AdjustOptOut();
            Debug.Log("### TTOptOut : Done OptOut");
        }

        static void DoOptIn()
        {
            Debug.Log("### TTOptOut : DoOptIn");
            MaxOptIn();
            IronSourceOptIn();
            AdMobOptIn();
            TenjinOptIn();
            GameAnalyticsOptIn();
            FirebaseOptIn();
            FacebookOptIn();
            UnityAnalyticsOptIn();
            AdjustOptIn();
            Debug.Log("### TTOptOut : Done OptIn");
        }

        public static void FacebookOptInOrOut()
        {
#if USE_FACEBOOK
            if (NeedOptOut())
            {
                FacebookOptOut();
            }
            else
            {
                FacebookOptIn();
            }
#endif
        }

#if USE_MAX
        // MAX -------
        // https://dash.applovin.com/documentation/mediation/unity/getting-started
        static void MaxOptIn()
        {
	        Debug.Log("### TTOptOut : MaxOptIn");
	        MaxSdk.SetDoNotSell(false);
	        MaxSdk.SetHasUserConsent(true);
        }
        static void MaxOptOut()
        {
	        Debug.Log("### TTOptOut : MaxOptOut");
	        MaxSdk.SetDoNotSell(true);
	        MaxSdk.SetHasUserConsent(false);
        }
#else
        static void MaxOptIn()
        {
        }

        static void MaxOptOut()
        {
        }
#endif

#if USE_IRONSOURCE
        // IronSource -------
        // https://developers.ironsrc.com/ironsource-mobile/unity/additional-sdk-settings/#step-1
        static void IronSourceOptIn()
        {
	        Debug.Log("### TTOptOut : IronSourceOptIn");
	        IronSource.Agent.setMetaData("do_not_sell", "false");
	        IronSource.Agent.setConsent(true);
        }
        static void IronSourceOptOut()
        {
	        Debug.Log("### TTOptOut : IronSourceOptOut");
	        IronSource.Agent.setMetaData("do_not_sell", "true");
	        IronSource.Agent.setConsent(false);
        }
#else
        static void IronSourceOptIn()
        {
        }

        static void IronSourceOptOut()
        {
        }
#endif

#if USE_ADMOB
        // AdMob -------
        // https://developers.google.cn/admob/ios/ccpa?hl=ja
        // https://developers.google.cn/admob/android/ccpa?hl=ja
        // SDK経由ではなくUserDefaultに保存することで対応できるらしい
        // NOTE: Unityでは未対応とのこと、Admobの初期化時にNeedOptOut()を参照し、Trueの場合は処理を行わないようにする
        // もしやる場合、リクエスト時には以下の方法で作ったAdRequestでやればOK?（ちょっと定かでは無い）
        // AdRequest request = new AdRequest.Builder().AddExtra("npa", "1").Build();
        // AdRequest request = new AdRequest.Builder().AddExtra("rdp", "1").Build();
        static void AdMobOptIn()
        {
	        Debug.Log("### TTOptOut : AdMobOptIn");
	        PlayerPrefs.DeleteKey("gad_rdp"); // iOS用
        }
        static void AdMobOptOut()
        {
	        Debug.Log("### TTOptOut : AdMobOptOut");
	        PlayerPrefs.SetInt("gad_rdp", 1); // iOS用 ※実際のオプトアウト対応で初期化も行わないのでいいかな。。。
        }
#else
        static void AdMobOptIn()
        {
        }

        static void AdMobOptOut()
        {
        }
#endif

#if USE_TENJIN
        // Tenjin -------
        // https://github.com/tenjin/tenjin-unity-sdk
        static void TenjinOptIn()
        {
            Debug.Log("### TTOptOut : TenjinOptIn");
            BaseTenjin instance = Tenjin.getInstance("T2WDDB18C9FSBMDZACZURGZ2ZUCYYQRZ");
            instance.OptIn();
            instance.Connect();
        }

        static void TenjinOptOut()
        {
            Debug.Log("### TTOptOut : TenjinOptOut");
            BaseTenjin instance = Tenjin.getInstance("T2WDDB18C9FSBMDZACZURGZ2ZUCYYQRZ");
            instance.OptOut();
            instance.Connect();
        }
#else
		static void TenjinOptIn() {}
        static void TenjinOptOut() {}
#endif

#if USE_GAMEANALYTICS
        // GameAnalytics -------
        // この他に過去12か月に収集された個人データの確認、開示、アクセス、または削除の要求など、特定の消費者の権利を行使する場合は
        // GAのプライバシーポリシーにメールアドレスがあり、それにユーザーが自分で依頼をする必要があるみたい
        // https://gameanalytics.com/privacy
        // ここでは送信しなくなるメソッドを呼ぶ
        static void GameAnalyticsOptIn()
        {
            Debug.Log("### TTOptOut : GameAnalyticeOptIn");
            GameAnalyticsSDK.GameAnalytics.SetEnabledEventSubmission(true);
        }

        static void GameAnalyticsOptOut()
        {
            Debug.Log("### TTOptOut : GameAnalyticsOptOut");
            GameAnalyticsSDK.GameAnalytics.SetEnabledEventSubmission(false);
        }
#else
		static void GameAnalyticsOptIn() {}
        static void GameAnalyticsOptOut() {}
#endif

#if USE_FIREBASE
        static void FirebaseOptIn()
        {
			Debug.Log("### TTOptOut : FirebaseOptIn");
	        Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        }
        static void FirebaseOptOut()
        {
			Debug.Log("### TTOptOut : FirebaseOptOut");
	        Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(false);
        }
#else
        static void FirebaseOptIn()
        {
        }

        static void FirebaseOptOut()
        {
        }
#endif

#if USE_FACEBOOK
        // Facebook -------
        // https://developers.facebook.com/docs/app-events/guides/ccpa
        // https://developers.facebook.com/docs/app-events/gdpr-compliance 
        // NOTE: FBの初期化が終わってない場合もあるので、FB.Init完了時に[TTOptOut.FacebookOptInOrOut()]の実装が必要
        static void FacebookOptIn()
        {
            Debug.Log("### TTOptOut : FacebookOptIn");
            if (!Facebook.Unity.FB.IsInitialized)
            {
                return;
            }

            Facebook.Unity.FB.Mobile.SetDataProcessingOptions(new string[] {"LDU"}, 1, 1000);
            Facebook.Unity.FB.Mobile.SetAutoLogAppEventsEnabled(true);
        }

        static void FacebookOptOut()
        {
            Debug.Log("### TTOptOut : FacebookOptOut");
            if (!Facebook.Unity.FB.IsInitialized)
            {
                return;
            }

            Facebook.Unity.FB.Mobile.SetDataProcessingOptions(new string[] { });
            Facebook.Unity.FB.Mobile.SetAutoLogAppEventsEnabled(false);
        }
#else
		static void FacebookOptIn() {}
        static void FacebookOptOut() {}
#endif

#if USE_UNITYANALYTICS
        // Unity Analytics（Unityのみ） -------
        // https://assetstore.unity.com/packages/add-ons/services/unity-data-privacy-plug-in-118922?locale=ja-JP
        // このアセットを組み込んで、アプリ内にボタンを用意する必要がある
        static void UnityAnalyticsOptIn()
        {
            Debug.Log("### TTOptOut : UnityAnalyticsOptIn");
            UnityEngine.Analytics.Analytics.enabled = true;
            UnityEngine.Analytics.Analytics.deviceStatsEnabled = true;
            UnityEngine.Analytics.Analytics.limitUserTracking = false;
#if UNITY_2018_3_OR_NEWER
            UnityEngine.Analytics.Analytics.initializeOnStartup = true;
#endif
        }

        static void UnityAnalyticsOptOut()
        {
            Debug.Log("### TTOptOut : UnityAnalyticsOptOut");
            UnityEngine.Analytics.Analytics.enabled = false;
            UnityEngine.Analytics.Analytics.deviceStatsEnabled = false;
            UnityEngine.Analytics.Analytics.limitUserTracking = true;
#if UNITY_2018_3_OR_NEWER
            UnityEngine.Analytics.Analytics.initializeOnStartup = false;
#endif
        }
#else
		static void UnityAnalyticsOptIn() {}
	    static void UnityAnalyticsOptOut() {}
#endif

#if USE_ADJUST
	    static void AdjustOptIn()
	    {
		    Debug.Log("### TTOptOut : AdjustOptIn");
		    com.adjust.sdk.Adjust.setEnabled(true);
	    }

	    static void AdjustOptOut()
	    {
		    Debug.Log("### TTOptOut : AdjustOptOut");
		    com.adjust.sdk.Adjust.setEnabled(false);
		    #if !UNITY_EDITOR
		    // AdjustのプラグインがEditorかどうかの判定を入れてくれてない。ひどい話ですよ。
		    com.adjust.sdk.Adjust.disableThirdPartySharing();
		    com.adjust.sdk.Adjust.gdprForgetMe();
			#endif
	    }
#else
        static void AdjustOptIn()
        {
        }

        static void AdjustOptOut()
        {
        }
#endif
    }

    static class TTOptOutPlayerPrefKeys
    {
#if UNITY_ANDROID
        public static string IsOptOut = "tt_optout";
#else
	    public static string IsOptOut = "__ttoptout";
#endif
    }
}