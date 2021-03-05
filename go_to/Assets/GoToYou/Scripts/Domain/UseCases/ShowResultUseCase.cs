using System;
using Common.Data.Signals;
using GoToYou.Data.Signals;
using Nimitools.CA.Domain;
using UniRx;

namespace GoToYou.Domain.UseCases
{
    public class ShowResultUseCase : UseCaseBase
    {
        Subject<ResultSignal> sendViewDataSubject = new Subject<ResultSignal>();
        public IObservable<ResultSignal> OnSendViewData => sendViewDataSubject;
        IMainRepository repository;

        public void SetRepository(IMainRepository repository)
        {
            this.repository = repository;
        }

        public override void Begin()
        {
            base.Begin();
            sendViewDataSubject.OnNext(null);
        }
    }
}