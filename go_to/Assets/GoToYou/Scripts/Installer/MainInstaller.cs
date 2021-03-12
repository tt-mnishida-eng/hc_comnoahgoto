using System;
using GoToYou.Adapter.Presenters;
using GoToYou.Adapter.Repositories;
using GoToYou.Data;
using GoToYou.Detail.DataStores;
using GoToYou.Detail.EventSender;
using GoToYou.Detail.Views;
using GoToYou.Domain.UseCases;
using Nimitools.CA.Detail;
using UnityEngine;

namespace GoToYou.Installer
{
    public class MainInstaller : MonoBehaviour
    {
        [SerializeField] PreparationView preparationView;
        [SerializeField] TitleView titleView;
        [SerializeField] PlayView playView;
        [SerializeField] ResultView resultView;

        [SerializeField] MainDataStore dataStore;
        [SerializeField] UseCaseConductor useCaseConductor;

        public void Install()
        {
            dataStore.Initialize();
            useCaseConductor.Initialize();

            var eventSender = new UnityAnalyticEventSender();
            var mainRepository = new MainRepository(dataStore);
            mainRepository.SetEventSender(eventSender);

            var preparationUs = new PreparationUseCase();
            preparationUs.SetRepository(mainRepository);
            var preparationPresenter = new PrepatationPresenter(preparationUs, preparationView);
            useCaseConductor.AddUseCase<UseCaseNames>(UseCaseNames.Preparation, preparationUs);

            var titleUS = new ShowTitleUseCase();
            titleUS.SetRepository(mainRepository);
            var titlePresenter = new TitlePresenter(titleUS, titleView);
            useCaseConductor.AddUseCase<UseCaseNames>(UseCaseNames.ShowTitle, titleUS);

            var playUS = new PlayUseCase();
            playUS.SetRepository(mainRepository);
            var playPresenter = new PlayPresenter(playUS, playView);
            useCaseConductor.AddUseCase<UseCaseNames>(UseCaseNames.Play, playUS);

            var resultUS = new ShowResultUseCase();
            resultUS.SetRepository(mainRepository);
            var resultPresenter = new ResultPresenter(resultUS, resultView);
            useCaseConductor.AddUseCase<UseCaseNames>(UseCaseNames.ShowResult, resultUS);
        }
    }
}