using Common.Adapter.Presenters.Interfaces;
using Common.Domain.UseCases;
using Nimitools.CA.Adapter;
using UniRx;

namespace Common.Adapter.Presenters
{
    public class CommonPlayPresenter : PresenterBase
    {
        CommonPlayUseCase useCase;

        ICommonPlayView view;

        public CommonPlayPresenter(CommonPlayUseCase useCase, ICommonPlayView view)
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