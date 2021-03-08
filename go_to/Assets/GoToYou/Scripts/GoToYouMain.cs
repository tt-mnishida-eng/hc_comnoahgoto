using System;
using GoToYou.Data;
using GoToYou.Installer;
using Nimitools.CA.Detail;
using Nimitools.CA.Signal;
using UnityEngine;

namespace GoToYou
{
    public class GoToYouMain : MonoBehaviour
    {
        [SerializeField] MainInstaller installer;
        [SerializeField] UseCaseConductor conductor;


        void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            installer.Install();

            var signal = new UsecaseSignal();
            signal.UseCaseEnum = (int) UseCaseNames.ShowTitle;
            conductor.Launch(signal);
        }
    }
}