using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class GameRootUI : MonoBehaviour
{
    [SerializeField]
    Image grayImage;

    [SerializeField]
    GameObject title;
    [SerializeField]
    GameObject play;
    [SerializeField]
    GameObject success;

    [SerializeField]
    GameObject failure;
    [SerializeField]
    Button tapToStartButton;
    public Button TapToStartButton => tapToStartButton;
    [SerializeField]
    Button retryButton;
    public Button RetryButton => retryButton;

    [SerializeField]
    Button nextStageButton;
    public Button NextStageButton => nextStageButton;

    [SerializeField]
    Image fadeImage;

    void Awake()
    {
        SetActive(grayImage.gameObject, false);
        SetActive(fadeImage.gameObject, false);
    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        SetActive(title.gameObject, false);
        SetActive(play.gameObject, false);
        SetActive(success.gameObject, false);
        SetActive(failure.gameObject, false);
    }

    public void BeTitleStyle()
    {
        SetActive(title.gameObject, true);
        SetActive(play.gameObject, false);
        SetActive(success.gameObject, false);
        SetActive(failure.gameObject, false);
    }
    public void BePlayStyle()
    {
        SetActive(title.gameObject, false);
        SetActive(play.gameObject, true);
        SetActive(success.gameObject, false);
        SetActive(failure.gameObject, false);
    }
    public void BeSuccessResultStyle()
    {
        SetActive(title.gameObject, false);
        SetActive(play.gameObject, false);
        SetActive(success.gameObject, true);
        SetActive(failure.gameObject, false);
    }
    public void BeFailureStyle()
    {
        SetActive(title.gameObject, false);
        SetActive(play.gameObject, false);
        SetActive(success.gameObject, false);
        SetActive(failure.gameObject, true);
    }


    public void FadeIn(Action onComplete)
    {
        // fadeImage.DOFade(0, 0.3f).OnComplete(() => { if (onComplete != null) onComplete(); });
    }
    public void FadeOut(Action onComplete)
    {
        // fadeImage.DOFade(1, 0.3f).OnComplete(() => { if (onComplete != null) onComplete(); });

    }
    void SetActive(GameObject gObj, bool isActive)
    {
        if (gObj == null) return;
        if (gObj.activeSelf != isActive)
            gObj.SetActive(isActive);
    }

}
