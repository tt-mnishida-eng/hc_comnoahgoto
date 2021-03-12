using System;
using UnityEngine;

namespace TT.Core.Bridge
{
    public class TTTenjin : SingletonMonoBehaviour<TTTenjin>
    {
        // 初回起動時間保持のKey
        const string _PrefsLaunchDateTimeKey = "ttcoretttenjinlaunchdatetime";
        private string _mApiKey;
        private bool _mIsInit;
        private bool _mEnableUpdateConversionValue;
        
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="apiKey">APIキー</param>
        /// <param name="enableUpdateConversionValue">
        /// ConversionValueの送信の有効状態
        /// falseを設定すると、ConversionValueの送信は行わない
        /// ※ アップデートで導入する場合、既存ユーザーはCVの送信を行いたくないための措置
        /// </param>
        /// <param name="conversionValue"></param>
        public void Init(string apiKey, bool enableUpdateConversionValue, int conversionValue)
        {
            _mIsInit = true;
            _mApiKey = apiKey;
            _mEnableUpdateConversionValue = enableUpdateConversionValue;
            RegisterAppForAdNetworkAttribution();
            Connect();
            UpdateConversionValue(conversionValue);
        }
        
        private void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                Connect();
            }
        }
        
        void Connect() {
            if (!_mIsInit)
            {
                return;
            }
            BaseTenjin instance = Tenjin.getInstance(_mApiKey);
            instance.Connect();
        }


        void RegisterAppForAdNetworkAttribution()
        {
            if (!_mIsInit)
            {
                return;
            }
#if UNITY_IOS
            BaseTenjin instance = Tenjin.getInstance(_mApiKey);
            instance.RegisterAppForAdNetworkAttribution();
#endif
        }
        
        public void UpdateConversionValue(int value)
        {
            if (!_mIsInit)
            {
                return;
            }
            // 24時間経過していたら送信しない
            DateTime? dateTime = null;
            if (PlayerPrefs.HasKey(_PrefsLaunchDateTimeKey))
            {
                try
                {
                    dateTime = DateTime.FromBinary (Convert.ToInt64 (PlayerPrefs.GetString (_PrefsLaunchDateTimeKey)));
                }
                catch(Exception ex)
                {
                    dateTime = null;
                }
            }
            if (!dateTime.HasValue)
            {
                dateTime = DateTime.Now;
                PlayerPrefs.SetString(_PrefsLaunchDateTimeKey, dateTime.Value.ToBinary().ToString());
                PlayerPrefs.Save();
            }
            var diff = DateTime.Now - dateTime.Value;
            if (diff.TotalHours >= 24)
            {
                return;
            }
            if (_mEnableUpdateConversionValue)
            {
                BaseTenjin instance = Tenjin.getInstance(_mApiKey);
                instance.UpdateConversionValue(value);
            }
        }
        
        public void SendEvent(string eventName)
        {
            if (!_mIsInit)
            {
                return;
            }
            TTDebug.Log("TTTenjin ==== SendEvent: " + eventName);
            BaseTenjin instance = Tenjin.getInstance(_mApiKey);
            instance.SendEvent(eventName);
        }
        
        public void SendEvent(string eventName, string eventValue)
        {
            if (!_mIsInit)
            {
                return;
            }
            TTDebug.Log("TTTenjin ==== SendEvent: " + eventName + "/" + eventValue);
            BaseTenjin instance = Tenjin.getInstance(_mApiKey);
            instance.SendEvent(eventName, eventValue);
        }
    }
}
