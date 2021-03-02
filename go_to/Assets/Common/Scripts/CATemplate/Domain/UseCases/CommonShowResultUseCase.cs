using System;
using Common.Data.Signals;
using Nimitools.CA.Domain;
using UniRx;

namespace Common.Domain.UseCases
{
    public class CommonShowResultUseCase : UseCaseBase
    {
        Subject<CommonResultSignal> sendViewDataSubject = new Subject<CommonResultSignal>();
        public IObservable<CommonResultSignal> OnSendViewData => sendViewDataSubject;
        IRepository repository;

        public void SetRepository(IRepository repository)
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