using System;
using System.Collections;
using System.Collections.Generic;
using TT;
using TT.Core.Bridge;
using UnityEngine;

namespace TT.Core.ConversionValue
{
    public class TTCVController : Singleton<TTCVController>
    {
        private TTCVCountData _mCountData;

        public delegate void DelegateWithValue(int value);

        public DelegateWithValue DelegateOnUpdateValue;

        public TTCVController()
        {
            _mCountData = new TTCVCountData();
            _mCountData.DispayBanner = TTCVCounter.GetValueCount(TTCVDefine.kValueType.DispayBanner);
            _mCountData.DispayRectangle = TTCVCounter.GetValueCount(TTCVDefine.kValueType.DispayRectangle);
            _mCountData.DispayInterstitial = TTCVCounter.GetValueCount(TTCVDefine.kValueType.DispayInterstitial);
            _mCountData.DispayReward = TTCVCounter.GetValueCount(TTCVDefine.kValueType.DispayReward);
            _mCountData.Purchase = TTCVCounter.GetValueCount(TTCVDefine.kValueType.Purchase);
        }

        public void SetupMediation(TTCVDefine.kMediationType mediationType)
        {
            switch (mediationType)
            {
                case TTCVDefine.kMediationType.AdMob:
                    TTDebug.LogWarning("各広告タイプのEventHandlerにデリゲート登録して、それぞれ取得してください");
                    break;
                case TTCVDefine.kMediationType.Max:
                    InitMax();
                    break;
                case TTCVDefine.kMediationType.IronSource:
                    TTDebug.LogWarning("ISのレクタンルを利用している場合は、バナーとレクタングルの判定ができないので、SetupMediationは利用せず独自でイベント設定してください");
                    InitIronSource();
                    break;
            }
        }

        public int GetValue()
        {
            return TTCVCalculator.Calc(_mCountData);
        }

        /// <summary>
        /// Valueタイプが行われた回数をインクリメントする
        /// MaxやISであればCallbackを設定しているので自動でカウントされるが
        /// 課金やAdMobのレクタングルなど、独自のものがある場合を考慮してpublicになっている
        /// </summary>
        /// <param name="kValueType"></param>
        public void IncrementValueCount(TTCVDefine.kValueType valueType)
        {
            var prevValue = GetValue();
            TTDebug.Log("==== TTCV: Increment: " + valueType);
            TTCVCounter.IncrementValueCount(valueType);
            _mCountData.Increment(valueType);
            if (prevValue != GetValue())
            {
                DidModifyCount();
            }
        }

        void InitMax()
        {
            //     // バナーの読み込み完了イベント
            //     MaxSdkCallbacks.OnBannerAdLoadedEvent += _ =>
            //     {
            //         IncrementValueCount(TTCVDefine.kValueType.DispayBanner);
            //     };
            //     // レクタングルの読み込み完了イベント
            //     MaxSdkCallbacks.OnMRecAdLoadedEvent += _ =>
            //     {
            //         IncrementValueCount(TTCVDefine.kValueType.DispayRectangle);
            //     };
            //     // インターステーシャルの表示完了イベント
            //     MaxSdkCallbacks.OnInterstitialDisplayedEvent += _ =>
            //     {
            //         IncrementValueCount(TTCVDefine.kValueType.DispayInterstitial);
            //     };
            //     // リワードの表示完了イベント
            //     MaxSdkCallbacks.OnRewardedAdDisplayedEvent += _ =>
            //     {   
            //         IncrementValueCount(TTCVDefine.kValueType.DispayReward);
            //     };
        }

        void InitIronSource()
        {
            // // バナーの読み込み完了イベント
            // IronSourceEvents.onBannerAdLoadedEvent += () =>
            // {
            //     //レクタングルなのかバナーなのかの判断がつかない。。。
            //     IncrementValueCount(TTCVDefine.kValueType.DispayBanner);
            // };
            // // インターステーシャルの表示完了イベント
            // IronSourceEvents.onInterstitialAdOpenedEvent += () =>
            // {
            //     IncrementValueCount(TTCVDefine.kValueType.DispayInterstitial);
            // };
            // // リワードの表示完了イベント
            // IronSourceEvents.onRewardedVideoAdOpenedEvent += () =>
            // {
            //     IncrementValueCount(TTCVDefine.kValueType.DispayReward);
            // };
        }

        void DidModifyCount()
        {
            var value = GetValue();
            TTTenjin.I.UpdateConversionValue(value);
            DelegateOnUpdateValue?.Invoke(value);
        }
    }
}