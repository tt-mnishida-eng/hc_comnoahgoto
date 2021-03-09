using Common.Extension;
using GoToYou.Adapter.Presenters.Interfaces;
using GoToYou.Data.Signals;
using GoToYou.Detail.GameStage;
using UnityEngine;

namespace GoToYou.Detail.Views
{
    public class PreparationView : MonoBehaviour, IPreparationView
    {
        [SerializeField] GoToYouStage goToYouStage;

        public void Begin()
        {
            this.SetActive(true);
        }

        public void Render(PreparationSignal signal)
        {
            goToYouStage.Initialize(signal.CurrentStageIndex);
        }

        public void End()
        {
            this.SetActive(false);
        }
    }
}