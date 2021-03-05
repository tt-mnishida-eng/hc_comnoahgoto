using Common.Adapter.Presenters.Interfaces;
using Common.Domain.UseCases;
using GoToYou.Adapter.Presenters.Interfaces;
using GoToYou.Data.Entity;
using GoToYou.Domain.UseCases;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters
{
    public class TitlePresenter : PresenterBase
    {
        ShowTitleUseCase useCase;

        ITitleView view;

        public TitlePresenter(ShowTitleUseCase useCase, ITitleView view)
        {
            this.useCase = useCase;
            this.view = view;
            Bind();
        }

        override protected void Bind()
        {
            useCase.OnBegin.Subscribe(x => view.Begin());
            useCase.OnEnd.Subscribe(x => view.End());
            view.OnTapToStart.Subscribe(x => useCase.TapStart());
        }
    }
}