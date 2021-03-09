using System;
using GoToYou.Data.Signals;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters.Interfaces
{
    public interface IResultView : IViewPort
    {
        IObservable<Unit> OnNextStage { get; }

        IObservable<Unit> OnRetry { get; }
        void Render(ResultSignal resultSignal);
    }
}