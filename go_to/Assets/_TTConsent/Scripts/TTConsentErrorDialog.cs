using System;
using System.Collections;
using System.Collections.Generic;
using TT.Consent;
using UnityEngine;

public class TTConsentErrorDialog : MonoBehaviour
{
    [SerializeField] private Animator _mAnimator;
    [SerializeField] private CanvasGroup _mCanvasGroup;

    public event EventHandler OnRetry;

    private void Awake()
    {
        _mCanvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        _mCanvasGroup.blocksRaycasts = true;
        _mAnimator.SetTrigger("Show");
    }
    public void Hide()
    {
        _mCanvasGroup.blocksRaycasts = false;
        _mAnimator.SetTrigger("Hide");
    }

    public void OnClickRetry()
    {
        OnRetry?.Invoke(this, EventArgs.Empty);
    }
}
