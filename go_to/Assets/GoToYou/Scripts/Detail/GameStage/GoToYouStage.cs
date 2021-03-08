using System;
using System.Collections.Generic;
using GoToYou.Detail.GameStage.Line;
using GoToYou.Detail.GameStage.People;
using GoToYou.Detail.GameStage.States;
using UnityEngine;

namespace GoToYou.Detail.GameStage
{
    public class GoToYouStage : MonoBehaviour
    {
        [SerializeField] AmidaMan amidaMan;
        public AmidaMan AmidaMan => amidaMan;
        FSM<GoToYouStage, GoToYouStates> fsm = new FSM<GoToYouStage, GoToYouStates>();

        [SerializeField] GameObject verticalLineContainer;
        public GameObject VerticalLineContainer => verticalLineContainer;
        List<AmidaLine> verticalLines = new List<AmidaLine>();

        [SerializeField] GameObject horizonLineContainer;
        List<AmidaLine> horizonLines = new List<AmidaLine>();

        [SerializeField] AmidaLine amidaLinePrefab;
        public GameObject HorizonLineContainer => horizonLineContainer;

        void Start()
        {
            Initialize();
        }

        void Update()
        {
            fsm.Update();
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

            var vLines = verticalLineContainer.GetComponentsInChildren<AmidaLine>();
            foreach (var amidaLine in vLines)
            {
                int lineIndex = verticalLines.Count;
                amidaLine.Initialize(lineIndex, AmidaLineType.Vertical);
                amidaLine.SetBeginNode(lineIndex, -1);
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
        }

        public AmidaLine AddHorizonLine(int verticalIndex)
        {
            var amidaLine = Instantiate<AmidaLine>(amidaLinePrefab, horizonLineContainer.transform);
            int horizonLineIndex = horizonLines.Count;
            amidaLine.Initialize(horizonLineIndex, AmidaLineType.Horizon);
            amidaLine.SetBeginNode(verticalIndex, horizonLineIndex);
            amidaLine.SetAmidaNodeCollidersEnabled(false);

            horizonLines.Add(amidaLine);
            return amidaLine;
        }

        public void WaitDraw()
        {
            fsm.Send(GoToYouStates.WaitDraw);
        }

        public void StartAmida()
        {
            fsm.Send(GoToYouStates.Amida);
        }

        public void Fail()
        {
            fsm.Send(GoToYouStates.Failure);
        }

        public void Success()
        {
            fsm.Send(GoToYouStates.Success);
        }
    }
}