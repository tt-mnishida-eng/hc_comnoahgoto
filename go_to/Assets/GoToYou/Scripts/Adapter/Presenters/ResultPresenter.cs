using Common.Adapter.Presenters.Interfaces;
using Common.Domain.UseCases;
using GoToYou.Domain.UseCases;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters
{
    public class ResultPresenter : PresenterBase
    {
        ShowResultUseCase useCase;

        ICommonResultView view;

        public ResultPresenter(ShowResultUseCase useCase, ICommonResultView view)
        {
            this.useCase = useCase;
            Bind();
        }

        override protected void Bind()
        {
            useCase.OnBegin.Subscribe(x => view.Begin());

            useCase.OnEnd.Subscribe(x => view.End());
        }
    }
}