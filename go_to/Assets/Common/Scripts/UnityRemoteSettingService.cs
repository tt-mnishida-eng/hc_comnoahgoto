using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnityRemoteSettingService
{
    private static class RemoteSettingKey
    {
        public const string InterstitialIntervalSecond = "interstitial_interval_second";
        public const string InterstitialStartSecond = "interstitial_start_second";
        public const string TestName = "test_name";
        public const string SheetName = "sheet_name";
        public const string SkinPercentagePerStage = "skin_percentage_per_stage";
        public const string SkinPercentageFirstSkin = "skin_percentage_first_skin";
    }

    private static class DefaultValue
    {
        public const int InterstitialIntervalSecond = 30;
        public const int InterstitialStartSecond = 30;
        public const string TestName = "none";
        public const string SheetName = "dev";
        public const float SkinPercentagePerStage = 25;
        public const float SkinPercentageFirstSkin = 33.4f;
    }

    // private readonly Action _remoteSettingLoaded;

    // public UnityRemoteSettingService(Action remoteSettingLoaded)
    // {
    //     _remoteSettingLoaded = remoteSettingLoaded;
    //     //Set up callback
    //     RemoteSettings.Completed += OnRemoteSettingCompleted;
    // }

    // private void OnRemoteSettingUpdated()
    // {
    //     _remoteSettingLoaded?.Invoke();
    // }

    // private void OnRemoteSettingCompleted(bool wasUpdatedFromServer, bool settingsChanged, int serverResponse)
    // {
    //     if (wasUpdatedFromServer)
    //     {
    //         Debug.LogWarning("RemoteSettings is downloaded. Http response code is : " + serverResponse);
    //     }
    //     _remoteSettingLoaded?.Invoke();
    // }

    public int GetInterstitialIntervalSecond()
    {
        return RemoteSettings.GetInt(RemoteSettingKey.InterstitialIntervalSecond,
            DefaultValue.InterstitialIntervalSecond);
    }

    public int GetInterstitialStartSecond()
    {
        return RemoteSettings.GetInt(RemoteSettingKey.InterstitialStartSecond, DefaultValue.InterstitialStartSecond);
    }
    public string GetSheetName()
    {
        return RemoteSettings.GetString(RemoteSettingKey.SheetName, DefaultValue.SheetName);
    }

    public string GetTestName()
    {
        return RemoteSettings.GetString(RemoteSettingKey.TestName, DefaultValue.TestName);
    }
    public float GetSkinPercentagePerStage()
    {
        return RemoteSettings.GetFloat(RemoteSettingKey.SkinPercentagePerStage, DefaultValue.SkinPercentagePerStage);
    }
    public float GetSkinPercentageFirstSkin()
    {
        return RemoteSettings.GetFloat(RemoteSettingKey.SkinPercentageFirstSkin, DefaultValue.SkinPercentageFirstSkin);
    }
    public Dictionary<string, object> GetAllSettings()
    {
        var settings = new Dictionary<string, object>
        {
            {RemoteSettingKey.InterstitialIntervalSecond, GetInterstitialIntervalSecond()},
            {RemoteSettingKey.InterstitialStartSecond, GetInterstitialStartSecond()},
            {RemoteSettingKey.SheetName, GetSheetName()},
            {RemoteSettingKey.TestName, GetTestName()},
            {RemoteSettingKey.SkinPercentagePerStage, GetSkinPercentagePerStage()},
            {RemoteSettingKey.SkinPercentageFirstSkin, GetSkinPercentageFirstSkin()}
        };
        return settings;
    }

}
