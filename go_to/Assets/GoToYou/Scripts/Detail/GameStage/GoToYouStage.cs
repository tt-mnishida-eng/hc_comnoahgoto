using System;
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

            fsm.Send(GoToYouStates.WaitDraw);
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