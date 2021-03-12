using System.Runtime.InteropServices;
using UnityEngine;

namespace TT.Core
{
    public static class TTDebug
    {
        public static bool IsDebug()
        {
            return _IsTTDebug();
        }

        public static void Log(object message)
        {
            if (IsDebug())
            {
                Debug.Log(message);
            }
        }

        public static void LogWarning(object message)
        {
            if (IsDebug())
            {
                Debug.LogWarning(message);
            }
        }

        public static void LogError(object message)
        {
            if (IsDebug())
            {
                Debug.LogError(message);
            }
        }

        #region DllImport
#if UNITY_IPHONE && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern bool _IsTTDebug();
#else
        private static bool _IsTTDebug() { return false; }
#endif
        #endregion // DllImport
    }
}

