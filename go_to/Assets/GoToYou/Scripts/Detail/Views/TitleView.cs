using System;
using GoToYou.Adapter.Presenters.Interfaces;
using Nimitools.CA.Adapter;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Common.Extension;

namespace GoToYou.Detail.Views
{
    public class TitleView : MonoBehaviour, ITitleView
    {
        [SerializeField] Button tapToStartButton;
        [SerializeField] Button optionButton;

        Subject<Unit> tapToStartSubject = new Subject<Unit>();
        public IObservable<Unit> OnTapToStart => tapToStartSubject;


        void Start()
        {
            tapToStartButton.onClick.AddListener(TapStart);
        }

        public void Begin()
        {
            this.SetActive(true);
        }

        public void End()
        {
            this.SetActive(false);
        }


        void TapStart()
        {
            tapToStartSubject.OnNext(Unit.Default);
        }
    }
}