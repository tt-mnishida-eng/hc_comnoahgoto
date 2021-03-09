using GoToYou.Data.Signals;
using GoToYou.Detail.GameStage;
using Nimitools.CA.Adapter;

namespace GoToYou.Adapter.Presenters.Interfaces
{
    public interface IPreparationView : IViewPort
    {
        GoToYouStage GoToYouStage { get; }
        void Render(PreparationSignal signal);
    }
}