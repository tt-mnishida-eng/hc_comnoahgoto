using System;
using GoToYou.Data;
using GoToYou.Data.Signals;
using Nimitools.CA.Domain;
using UniRx;

namespace GoToYou.Domain.UseCases
{
    public class PreparationUseCase : UseCaseBase
    {
        Subject<PreparationSignal> sendViewDataSubject = new Subject<PreparationSignal>();
        public IObservable<PreparationSignal> OnSendViewData => sendViewDataSubject;
        IMainRepository repository;

        public void SetRepository(IMainRepository repository)
        {
            this.repository = repository;
        }

        public override void Begin()
        {
            base.Begin();
            var userEntity = repository.GetUserEntity();

            var preparationSignal = new PreparationSignal();
            preparationSignal.Progress = userEntity.Progress;
            sendViewDataSubject.OnNext(preparationSignal);
        }

        public override void End()
        {
            base.End();
            SendUseCaseAtIndex((int) UseCaseNames.ShowTitle);
        }
    }
}