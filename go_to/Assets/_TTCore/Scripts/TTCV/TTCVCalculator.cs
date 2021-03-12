using System;
using UnityEngine;

namespace TT.Core.ConversionValue
{
    public static class TTCVCalculator
    {
        /// <summary>
        /// ConversionValueを計算して返す
        /// ①イベントカウントと計算
        ///     ・バナー表示回数×0.3
        ///     ・レクタングル表示回数×3
        ///     ・インステ表示回数×20
        ///     ・リワード表示回数×40
        ///     ・課金回数×1000
        /// ②上記を全て合計
        /// ③合計した数値×0.1
        /// ④算出した結果を四捨五入し整数にしたものがConversionValue
        /// </summary>
        /// <returns></returns>
        public static int Calc(TTCVCountData countData)
        {
            var totalValue = 0.0f;
            totalValue += countData.DispayBanner * 0.3f;
            totalValue += countData.DispayRectangle * 3.0f;
            totalValue += countData.DispayInterstitial * 20.0f;
            totalValue += countData.DispayReward * 40.0f;
            totalValue += countData.Purchase * 1000.0f;

            var result = Math.Min(Mathf.RoundToInt(totalValue * 0.1f), 63);
            if (TTDebug.IsDebug())
            {
                var logText = "";
                logText += "==== TTCV: Calc Start\n";
                logText += "           Count DispayBanner: " + countData.DispayBanner + "\n";
                logText += "           Count DispayRectangle: " + countData.DispayRectangle + "\n";
                logText += "           Count DispayInterstitial: " + countData.DispayInterstitial + "\n";
                logText += "           Count DispayReward: " + countData.DispayReward + "\n";
                logText += "           Count Purchase: " + countData.Purchase + "\n";
                logText += "           Count TotalValue: " + totalValue + "\n";
                logText += "           Result: " + result;
                TTDebug.Log(logText);
            }
            return result;
        }
    }
}