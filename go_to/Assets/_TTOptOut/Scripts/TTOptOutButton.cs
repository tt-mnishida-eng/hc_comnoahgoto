using System;
using System.Collections;
using System.Collections.Generic;
using TT.Consent;
using UnityEngine;

namespace TT.OptOut
{
    public class TTOptOutButton : MonoBehaviour
    {
        [SerializeField] private GameObject _mBtnOnObject;
        [SerializeField] private GameObject _mBtnOffObject;

        private void Start()
        {
            if (!TTConsent.AlreadyShowConsent())
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                DrawToggle();
            }
        }

        void DrawToggle()
        {
            if (TTOptOut.NeedOptOut())
            {
                _mBtnOnObject.SetActive(false);
                _mBtnOffObject.SetActive(true);
            }
            else
            {
                _mBtnOnObject.SetActive(true);
                _mBtnOffObject.SetActive(false);
            }
        }

        public void OnClick()
        {
            TTOptOut.SetNeedOptOut(!TTOptOut.NeedOptOut());
            DrawToggle();
        }
    }

}

