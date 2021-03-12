using System;
using UnityEngine;

namespace TT.Core.ConversionValue
{
    public static class TTCVCounter
    {
        /// <summary>
        /// Valueタイプが行われた回数を返す
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static int GetValueCount(TTCVDefine.kValueType valueType)
        {
            return PlayerPrefs.GetInt(GetValueCountPrefsKey(valueType), 0);
        }
        
        /// <summary>
        /// Valueタイプが行われた回数をインクリメントする
        /// </summary>
        /// <param name="kValueType"></param>
        public static void IncrementValueCount(TTCVDefine.kValueType valueType)
        {
            var count = GetValueCount(valueType);
            count++;
            PlayerPrefs.SetInt(GetValueCountPrefsKey(valueType), count);
            PlayerPrefs.Save();
        }

        static string GetValueCountPrefsKey(TTCVDefine.kValueType valueType)
        {
            return "ttcorettcvcalculator" + valueType.ToString().ToLower() + "count";
        }
    }
}