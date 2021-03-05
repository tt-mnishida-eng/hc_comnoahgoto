using System;
using Common.Extension;
using GoToYou.Adapter.Presenters.Interfaces;
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

        Subject<Unit> successSubject = new Subject<Unit>();
        public IObservable<Unit> OnSuccess => successSubject;

        Subject<Unit> failSubject = new Subject<Unit>();
        public IObservable<Unit> OnFail => failSubject;

        void Start()
        {
            retryButton.onClick.AddListener(Retry);
        }

        public void Begin()
        {
            this.SetActive(true);
        }

        public void End()
        {
            this.SetActive(false);
        }

        void Retry()
        {
            retrySubject.OnNext(Unit.Default);
        }
    }
}