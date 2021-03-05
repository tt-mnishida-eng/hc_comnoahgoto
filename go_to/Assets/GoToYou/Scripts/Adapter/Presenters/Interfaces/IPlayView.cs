using System;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters.Interfaces
{
    public interface IPlayView : IViewPort
    {
        IObservable<Unit> OnFail { get; }
        IObservable<Unit> OnSuccess { get; }

        IObservable<Unit> OnRetry { get; }
    }
}