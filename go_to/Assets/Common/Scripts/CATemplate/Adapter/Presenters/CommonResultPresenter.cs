using Common.Adapter.Presenters.Interfaces;
using Common.Domain.UseCases;
using Nimitools.CA.Adapter;
using UniRx;

namespace Common.Adapter.Presenters
{
    public class CommonResultPresenter : PresenterBase
    {
        CommonShowResultUseCase useCase;

        ICommonResultView view;

        public CommonResultPresenter(CommonShowResultUseCase useCase, ICommonResultView view)
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