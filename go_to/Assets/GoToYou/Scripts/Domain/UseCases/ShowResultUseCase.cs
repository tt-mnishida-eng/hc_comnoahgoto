using System;
using Common.Data.Signals;
using GoToYou.Data;
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
            var userEntity = repository.GetUserEntity();
            var resultSignal = new ResultSignal();
            resultSignal.IsSuccess = userEntity.IsCurrentStageSuccess;
            sendViewDataSubject.OnNext(resultSignal);
        }

        public override void End()
        {
            base.End();
        }

        public void GotoNextStage()
        {
            repository.SendLevelEndEvent();
            var userEntity = repository.GetUserEntity();
            userEntity.Progress++;
            End();
            GameRootManager.Instance.ReloadScene();
        }

        public void Retry()
        {
            End();
            GameRootManager.Instance.ReloadScene();
        }
    }
}