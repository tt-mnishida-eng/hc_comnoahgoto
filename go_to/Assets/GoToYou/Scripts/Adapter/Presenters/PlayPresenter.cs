using Common.Adapter.Presenters.Interfaces;
using Common.Domain.UseCases;
using GoToYou.Adapter.Presenters.Interfaces;
using GoToYou.Domain.UseCases;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters
{
    public class PlayPresenter : PresenterBase
    {
        PlayUseCase useCase;

        IPlayView view;

        public PlayPresenter(PlayUseCase useCase, IPlayView view)
        {
            this.useCase = useCase;
            this.view = view;
            Bind();
        }

        override protected void Bind()
        {
            useCase.OnBegin.Subscribe(x => view.Begin());

            useCase.OnEnd.Subscribe(x => view.End());
            view.OnRetry.Subscribe(x => useCase.Retry());
            view.GoToYouStage.OnSuccess.Subscribe(x => useCase.Success());
            view.GoToYouStage.OnFail.Subscribe(x => useCase.Fail());
        }
    }
}