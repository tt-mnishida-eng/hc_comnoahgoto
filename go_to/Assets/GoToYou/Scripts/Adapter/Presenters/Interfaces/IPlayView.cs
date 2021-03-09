using System;
using GoToYou.Detail.GameStage;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters.Interfaces
{
    public interface IPlayView : IViewPort
    {
        GoToYouStage GoToYouStage { get; }
        IObservable<Unit> OnRetry { get; }
    }
}