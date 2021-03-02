using Common.Adapter.Presenters.Interfaces;
using Common.Domain.UseCases;
using GoToYou.Domain.UseCases;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters
{
    public class PlayPresenter : PresenterBase
    {
        PlayUseCase useCase;

        ICommonPlayView view;

        public PlayPresenter(PlayUseCase useCase, ICommonPlayView view)
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