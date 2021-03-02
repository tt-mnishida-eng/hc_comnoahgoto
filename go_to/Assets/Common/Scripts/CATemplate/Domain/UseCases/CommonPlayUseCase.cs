using System;
using Common.Data.Signals;
using Nimitools.CA.Domain;
using UniRx;

namespace Common.Domain.UseCases
{
    public class CommonPlayUseCase : UseCaseBase
    {
        Subject<CommonPlaySignal> sendViewDataSubject = new Subject<CommonPlaySignal>();
        public IObservable<CommonPlaySignal> OnSendViewData => sendViewDataSubject;
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