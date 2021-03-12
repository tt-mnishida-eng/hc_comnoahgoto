using System;
using System.Collections.Generic;
using GoToYou.Detail.GameStage.Line;
using GoToYou.Detail.GameStage.People;
using GoToYou.Detail.GameStage.States;
using UniRx;
using UnityEngine;

namespace GoToYou.Detail.GameStage
{
    public class GoToYouStage : MonoBehaviour
    {
        [SerializeField] StageContainer stageContainer;

        [SerializeField] AmidaMan amidaMan;
        public AmidaMan AmidaMan => amidaMan;
        [SerializeField] PersonInNeed personInNeed;
        public PersonInNeed PersonInNeed => personInNeed;
        FSM<GoToYouStage, GoToYouStates> fsm = new FSM<GoToYouStage, GoToYouStates>();

        [SerializeField] GameObject verticalLineContainer;
        public GameObject VerticalLineContainer => verticalLineContainer;

        List<AmidaLine> verticalLines = new List<AmidaLine>();

        List<AmidaLine> horizonLines = new List<AmidaLine>();

        [SerializeField] GameObject horizonLineContainer;
        public GameObject HorizonLineContainer => horizonLineContainer;

        [SerializeField] AmidaLine amidaLinePrefab;

        Subject<Unit> successSubject = new Subject<Unit>();
        public IObservable<Unit> OnSuccess => successSubject;

        Subject<Unit> failSubject = new Subject<Unit>();
        public IObservable<Unit> OnFail => failSubject;

        Subject<Unit> finishSetProgressSubject = new Subject<Unit>();
        public IObservable<Unit> OnFinishSetProgress => finishSetProgressSubject;

        [SerializeField] ParticleSystem digParticle;

        public ParticleSystem DigParticle => digParticle;

        public bool Available { get; set; } = false;

        void Awake()
        {
            digParticle.Stop();
        }

        void Start()
        {
            Initialize();
        }


        void Update()
        {
            fsm.Update();
        }

        public void SetProgress(int progress)
        {
            stageContainer.SetProgress(progress);
            amidaMan.FirstPositeion = stageContainer.FirstPosition;
            verticalLineContainer = stageContainer.CurrentVerticalLineContainer;
            horizonLineContainer = stageContainer.CurrentHorizonLineContainer;
            var vLines = verticalLineContainer.GetComponentsInChildren<AmidaLine>();
            foreach (var amidaLine in vLines)
            {
                int lineIndex = verticalLines.Count;
                amidaLine.Initialize(lineIndex, AmidaLineType.Vertical);
                amidaLine.SetBeginNode(lineIndex, -2);
                amidaLine.SetEndNode(lineIndex, -1);
                verticalLines.Add(amidaLine);
            }

            var hLines = horizonLineContainer.GetComponentsInChildren<AmidaLine>();
            foreach (var amidaLine in hLines)
            {
                int lineIndex = horizonLines.Count;
                amidaLine.Initialize(lineIndex, AmidaLineType.Horizon);
                amidaLine.SetAmidaNodeCollidersEnabled(true);
                horizonLines.Add(amidaLine);
            }

            fsm.Send(GoToYouStates.WaitDraw);
            finishSetProgressSubject.OnNext(Unit.Default);
        }

        public void Reset()
        {
            amidaMan.Reset();
            stageContainer.Reset();
        }

        void Initialize()
        {
            var waitdrawState = new WaitDrawState(this);
            fsm.AddState(GoToYouStates.WaitDraw, waitdrawState, true);

            var drawState = new DrawState(this);
            fsm.AddState(GoToYouStates.Draw, drawState, true);

            var amidaState = new AmidaState(this);
            fsm.AddState(GoToYouStates.Amida, amidaState, true);

            var failureState = new FailureState(this);
            fsm.AddState(GoToYouStates.Failure, failureState, true);

            var successState = new SuccessState(this);
            fsm.AddState(GoToYouStates.Success, successState, true);
        }

        public AmidaLine AddHorizonLine(int verticalIndex)
        {
            var amidaLine = Instantiate<AmidaLine>(amidaLinePrefab, horizonLineContainer.transform);
            int horizonLineIndex = horizonLines.Count;
            amidaLine.Initialize(horizonLineIndex, AmidaLineType.Horizon);
            amidaLine.SetBeginNode(verticalIndex, horizonLineIndex);
            amidaLine.SetAmidaNodeCollidersEnabled(false);
            Color clr;

            if (ColorUtility.TryParseHtmlString("#FF9A31", out clr))
            {
                amidaLine.SetColor(clr);
            }

            horizonLines.Add(amidaLine);
            return amidaLine;
        }

        public void WaitDraw()
        {
            stageContainer.StartMove();
            fsm.Send(GoToYouStates.WaitDraw);
        }

        public void StartAmida()
        {
            fsm.Send(GoToYouStates.Amida);
        }

        public void Fail()
        {
            fsm.Send(GoToYouStates.Failure);
            failSubject.OnNext(Unit.Default);
        }

        public void Success()
        {
            fsm.Send(GoToYouStates.Success);
            successSubject.OnNext(Unit.Default);
        }
    }
}