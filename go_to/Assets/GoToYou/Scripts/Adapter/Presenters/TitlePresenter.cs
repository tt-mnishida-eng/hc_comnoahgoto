using Common.Adapter.Presenters.Interfaces;
using Common.Domain.UseCases;
using GoToYou.Domain.UseCases;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters
{
    public class TitlePresenter : PresenterBase
    {
        ShowTitleUseCase useCase;

        ICommonTitleView view;

        public TitlePresenter(ShowTitleUseCase useCase, ICommonTitleView view)
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