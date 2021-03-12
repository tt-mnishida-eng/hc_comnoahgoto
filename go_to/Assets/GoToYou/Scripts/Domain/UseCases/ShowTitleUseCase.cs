using System;
using GoToYou.Data;
using GoToYou.Data.Signals;
using Nimitools.CA.Domain;
using UniRx;

namespace GoToYou.Domain.UseCases
{
    public class ShowTitleUseCase : UseCaseBase
    {
        Subject<TitleSignal> sendViewDataSubject = new Subject<TitleSignal>();
        public IObservable<TitleSignal> OnSendViewData => sendViewDataSubject;
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

        public void TapStart()
        {
            repository.SendLevelStartEvent();
            repository.SendGameStartEvent();
            base.End();
            SendUseCaseAtIndex((int) UseCaseNames.Play);
        }
    }
}