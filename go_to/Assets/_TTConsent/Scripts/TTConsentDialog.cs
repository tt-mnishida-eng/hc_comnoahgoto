//#define NEES_DRAW_THRIDPARTY
#define USE_GAMEANALYTICS

using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TT.Consent
{
    public class TTConsentDialog : MonoBehaviour
    {
        [SerializeField] TMP_Text _mMessageText = null, _mDeveloperText = null;
        [SerializeField] GameObject _mPrefabThirdPartyTextObj = null, _mPrefabThirdPartySpaceObj = null;
        [SerializeField] GameObject _mScrollContentsObj = null;

        Animator _mAnimator;
        bool _mUserInteractionEnabled = false;
        bool _mIsConsent = false;
        public event EventHandler<TTConsentDialogEventArgs> OnConsent;

#if NEES_DRAW_THRIDPARTY
        [System.Serializable]
        public class GdprData
        {
            public string m;
            public string d;
            public string[] t;
        }
#endif

        private void Start()
        {
            _mAnimator = GetComponent<Animator>();
        }

        public void Show()
        {
            _mAnimator.SetTrigger("Show");
        }

        public void Setup(kDeveloper developerType, string text)
        {
#if NEES_DRAW_THRIDPARTY
            GdprData gdprData;
            try
            {
                gdprData = JsonUtility.FromJson<GdprData>(text);
            }
            catch (Exception)
            {
                gdprData = new GdprData();
                gdprData.m = _mMessageText.text;
                gdprData.d = _mDeveloperText.text;
                gdprData.t = new string[0];
            }

            var messageText = gdprData.m;
#else
            var messageText = text;
#endif
            messageText = messageText.Replace("%D_NAME%", TTConsentDefine.GetDeveloperName(developerType));
            _mMessageText.text = messageText;

            var developerText = _mDeveloperText.text;
            developerText = developerText.Replace("%D_NAME%", TTConsentDefine.GetDeveloperName(developerType));
            developerText = developerText.Replace("%P_LINK%", TTConsentDefine.GetPrivacyUrl(developerType));
            developerText = developerText.Replace("%T_LINK%", TTConsentDefine.GetTermsUrl(developerType));
            _mDeveloperText.text = developerText;

#if NEES_DRAW_THRIDPARTY
            foreach (var thirdPartyText in gdprData.t)
            {
                var textObj = Instantiate(_mPrefabThirdPartyTextObj);
                textObj.GetComponent<TMP_Text>().text = thirdPartyText;
                textObj.transform.SetParent(_mScrollContentsObj.transform, false);

                var spaceObj = Instantiate(_mPrefabThirdPartySpaceObj);
                spaceObj.transform.SetParent(_mScrollContentsObj.transform, false);
            }
#else
            LayoutRebuilder.ForceRebuildLayoutImmediate(_mScrollContentsObj.GetComponent<RectTransform>());
            
#endif
        }

        public void OnClickAgree()
        {
            if (!_mUserInteractionEnabled)
            {
                return;
            }

            _mUserInteractionEnabled = false;
            _mIsConsent = true;
            Hide();
        }
        
        public void OnClickDisagree()
        {
            if (!_mUserInteractionEnabled)
            {
                return;
            }

            _mUserInteractionEnabled = false;
            _mIsConsent = false;
            Hide();
        }

        void Hide()
        {
            var e = new TTConsentDialogEventArgs();
            e.IsConsent = _mIsConsent;
            OnConsent?.Invoke(this, e);
            //Destroy(this.gameObject);
        }
        

        public void DidShow_FrameEvent()
        {
            _mUserInteractionEnabled = true;
        }

        public void DidHide_FrameEvent()
        {
            /*
            var e = new TTConsentDialogEventArgs();
            e.IsConsent = _mIsConsent;
            OnConsent?.Invoke(this, e);
            Destroy(this.gameObject);
            */
        }
    }

    public class TTConsentDialogEventArgs : EventArgs
    {
        public bool IsConsent { get; set; }
    }
}