using GoToYou.Data.Signals;
using Nimitools.CA.Adapter;

namespace GoToYou.Adapter.Presenters.Interfaces
{
    public interface IPreparationView : IViewPort
    {
        void Render(PreparationSignal signal);
    }
}