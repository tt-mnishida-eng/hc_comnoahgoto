using System;
using GoToYou.Adapter.Presenters;
using GoToYou.Adapter.Repositories;
using GoToYou.Data;
using GoToYou.Detail.DataStores;
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

            var mainRepository = new MainRepository(dataStore);

            var preparationUS = new PreparationUseCase();
            preparationUS.SetRepository(mainRepository);
            var preparationPresenter = new PrepatationPresenter(preparationUS, preparationView);
            useCaseConductor.AddUseCase<UseCaseNames>(UseCaseNames.Preparation, preparationUS);

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