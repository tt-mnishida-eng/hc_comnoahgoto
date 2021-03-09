using System;
using Common.Extension;
using GoToYou.Adapter.Presenters.Interfaces;
using GoToYou.Data.Signals;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoToYou.Detail.Views
{
    public class ResultView : MonoBehaviour, IResultView
    {
        [SerializeField] GameObject success;
        [SerializeField] Button nextStageButton;

        Subject<Unit> nextStageSubject = new Subject<Unit>();
        public IObservable<Unit> OnNextStage => nextStageSubject;

        [SerializeField] GameObject failure;
        [SerializeField] Button retryButton;
        Subject<Unit> retrySubject = new Subject<Unit>();
        public IObservable<Unit> OnRetry => retrySubject;


        void Start()
        {
            nextStageButton.onClick.AddListener(TapNextStage);
            retryButton.onClick.AddListener(TapRetry);
        }

        public void Begin()
        {
            this.SetActive(true);
        }

        public void Render(ResultSignal resultSignal)
        {
            success.SetActive(resultSignal.IsSuccess);
            failure.SetActive(!resultSignal.IsSuccess);
        }

        public void End()
        {
            this.SetActive(false);
        }

        void TapNextStage()
        {
            nextStageSubject.OnNext(Unit.Default);
        }

        void TapRetry()
        {
            retrySubject.OnNext(Unit.Default);
        }
    }
}