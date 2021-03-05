using System;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters.Interfaces
{
    public interface ITitleView : IViewPort
    {
        IObservable<Unit> OnTapToStart { get; }
    }
}