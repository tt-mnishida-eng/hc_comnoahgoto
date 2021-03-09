using GoToYou.Adapter.Presenters.Interfaces;
using GoToYou.Domain.UseCases;
using Nimitools.CA.Adapter;
using UniRx;

namespace GoToYou.Adapter.Presenters
{
    public class PrepatationPresenter : PresenterBase
    {
        PreparationUseCase useCase;

        IPreparationView view;

        public PrepatationPresenter(PreparationUseCase useCase, IPreparationView view)
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
        }
    }
}