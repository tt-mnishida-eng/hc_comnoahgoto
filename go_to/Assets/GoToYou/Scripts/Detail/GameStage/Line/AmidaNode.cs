using System;
using UniRx;
using UnityEngine;

namespace GoToYou.Detail.GameStage.Line
{
    public class AmidaNode : MonoBehaviour
    {
        [SerializeField] int verticalLineIndex = -1;

        public int VerticalLineIndex
        {
            get => verticalLineIndex;
            set => verticalLineIndex = value;
        }

        [SerializeField] int horizonLineIndex = -1;


        public int HorizonLineIndex
        {
            get => horizonLineIndex;
            set => horizonLineIndex = value;
        }

        [SerializeField] BoxCollider boxCollider;

        public BoxCollider BoxCollider => boxCollider;

        Subject<AmidaNode> startCrossSubject = new Subject<AmidaNode>();
        public IObservable<AmidaNode> OnStartCross => startCrossSubject;

        public void TriggerStartCross()
        {
            startCrossSubject.OnNext(this);
        }

        Subject<AmidaNode> endCrossSubject = new Subject<AmidaNode>();
        public IObservable<AmidaNode> OnEndCross => endCrossSubject;

        public void TriggerEndCross()
        {
            endCrossSubject.OnNext(this);
        }
    }
}