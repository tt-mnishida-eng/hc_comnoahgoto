using System;
using GoToYou.Adapter.Presenters;
using GoToYou.Adapter.Repositories;
using GoToYou.Data;
using GoToYou.Detail.Views;
using GoToYou.Domain.UseCases;
using Nimitools.CA.Detail;
using UnityEngine;

namespace GoToYou.Installer
{
    public class MainInstaller : MonoBehaviour
    {
        [SerializeField] TitleView titleView;
        [SerializeField] PlayView playView;
        [SerializeField] ResultView resultView;

        [SerializeField] UseCaseConductor useCaseConductor;

        public void Install()
        {
            useCaseConductor.Initialize();

            var mainRepository = new MainRepository();

            var titleUS = new ShowTitleUseCase();
            titleUS.SetRepository(mainRepository);
            var titlePresenter = new TitlePresenter(titleUS, titleView);
            useCaseConductor.AddUseCase<UseCaseNames>(UseCaseNames.ShowTitle, titleUS);

            var playUS = new PlayUseCase();
            playUS.SetRepository(mainRepository);
            var playPresenter = new PlayPresenter(playUS, playView);
            useCaseConductor.AddUseCase<UseCaseNames>(UseCaseNames.Play, playUS);

            var resultUS = new ShowResultUseCase();
            playUS.SetRepository(mainRepository);
            var resultPresenter = new ResultPresenter(resultUS, resultView);
            useCaseConductor.AddUseCase<UseCaseNames>(UseCaseNames.ShowResult, resultUS);
        }
    }
}