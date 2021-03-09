using Common.Adapter.Presenters.Interfaces;
using Common.Domain.UseCases;
using GoToYou.Adapter.Presenters.Interfaces;
using GoToYou.Domain.UseCases;
using Nimitools.CA.Adapter;
using UniRx;
using UnityEngine.PlayerLoop;

namespace GoToYou.Adapter.Presenters
{
    public class ResultPresenter : PresenterBase
    {
        ShowResultUseCase useCase;

        IResultView view;

        public ResultPresenter(ShowResultUseCase useCase, IResultView view)
        {
            this.useCase = useCase;
            this.view = view;
            Bind();
        }

        override protected void Bind()
        {
            useCase.OnBegin.Subscribe(x => view.Begin());

            useCase.OnEnd.Subscribe(x => view.End());
            useCase.OnSendViewData.Subscribe(x => view.Render(x));
            view.OnRetry.Subscribe(x => useCase.Retry());
            view.OnNextStage.Subscribe(x => useCase.GotoNextStage());
        }
    }
}