using System;
using GoToYou.Data.Signals;
using Nimitools.CA.Domain;
using UniRx;

namespace GoToYou.Domain.UseCases
{
    public class PlayUseCase : UseCaseBase
    {
        Subject<PlaySignal> sendViewDataSubject = new Subject<PlaySignal>();
        public IObservable<PlaySignal> OnSendViewData => sendViewDataSubject;
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