using System;
using GoToYou.Data;
using GoToYou.Data.Signals;
using Nimitools.CA.Domain;
using UniRx;

namespace GoToYou.Domain.UseCases
{
    public class PlayUseCase : UseCaseBase
    {
        Subject<PlaySignal> sendViewDataSubject = new Subject<PlaySignal>();
        public IObservable<PlaySignal> OnSendViewData => sendViewDataSubject;
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

        public void Success()
        {
            base.End();
            SendUseCaseAtIndex((int) UseCaseNames.ShowResult);
        }

        public void Fail()
        {
            base.End();
            SendUseCaseAtIndex((int) UseCaseNames.ShowResult);
        }
    }
}