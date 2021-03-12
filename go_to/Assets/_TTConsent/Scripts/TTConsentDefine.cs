using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT.Consent
{
    public enum kDeveloper
    {
        Fty,
        Babangida
    }

    public static class TTConsentDefine
    {
        public static string GetApiUrl(kDeveloper developerType, bool debug = false)
        {
            string url = "";
            if (developerType == kDeveloper.Babangida)
            {
                url = "https://ccpa.babangida.be/apps/tracking/";
            }
            else
            {
                url = "https://ccpa.ftyapp.com/apps/tracking/";
            }
            url += "?nocache=" + DateTime.Now.Millisecond;
            if (debug)
            {
                //url += "&debug_area=California";
                url += "&debug_area=FR";
            }
            return url;
        }

        public static string GetDeveloperName(kDeveloper developerType)
        {
            if (developerType == kDeveloper.Babangida)
            {
                return "Babangida LLC.";
            }
            else
            {
                return "FTY LLC.";
            }
        }

        public static string GetTermsUrl(kDeveloper developerType)
        {
            if (developerType == kDeveloper.Babangida)
            {
                return "https://app.babangida.be/rule/en.php";
            }
            else
            {
                return "https://app.ftyapp.com/rule/en.php";
            }
        }
        
        public static string GetPrivacyUrl(kDeveloper developerType)
        {
            if (developerType == kDeveloper.Babangida)
            {
                return "https://app.babangida.be/privacy/en.php";
            }
            else
            {
                return "https://app.ftyapp.com/privacy/en.php";
            }
        }
    }
    
    [Serializable]
    public struct ThirdPartyLinks
    {
        public string TermsUrl;
        public string PrivacyUrl;
    }
}
