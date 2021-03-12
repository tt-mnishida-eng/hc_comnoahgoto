using System;
using Common.Extension;
using GoToYou.Adapter.Presenters.Interfaces;
using GoToYou.Detail.GameStage;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoToYou.Detail.Views
{
    public class PlayView : MonoBehaviour, IPlayView
    {
        [SerializeField] Button retryButton;

        Subject<Unit> retrySubject = new Subject<Unit>();
        public IObservable<Unit> OnRetry => retrySubject;


        [SerializeField] GoToYouStage goToYouStage;

        public GoToYouStage GoToYouStage => goToYouStage;

        void Start()
        {
            retryButton.onClick.AddListener(Retry);
        }

        public void Begin()
        {
            this.SetActive(true);
            goToYouStage.Available = true;
            goToYouStage.WaitDraw();
        }

        public void End()
        {
            goToYouStage.Available = false;
            this.SetActive(false);
        }

        void Retry()
        {
            retrySubject.OnNext(Unit.Default);
        }
    }
}