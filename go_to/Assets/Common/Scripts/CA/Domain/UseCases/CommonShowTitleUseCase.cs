using System;
using Common.Data.Signals;
using Nimitools.CA.Domain;
using UniRx;

namespace Common.Domain.UseCases
{
    public class CommonShowTitleUseCase : UseCaseBase
    {
        Subject<CommonTitleSignal> sendViewDataSubject = new Subject<CommonTitleSignal>();
        public IObservable<CommonTitleSignal> OnSendViewData => sendViewDataSubject;
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