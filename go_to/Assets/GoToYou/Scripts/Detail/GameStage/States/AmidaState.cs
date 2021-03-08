//  AmidaState.cs

using System.Collections.Generic;
using UnityEngine;
using System;
using GoToYou.Detail.GameStage.People;
using UnityEditor;

namespace GoToYou.Detail.GameStage.States
{
    /// <summary>
    /// 
    /// StateBehaviour
    /// </summary>
    public sealed class AmidaState : StateBase<GoToYouStage, GoToYouStates>
    {
        AmidaMan amidaMan;

        public AmidaState(GoToYouStage context) : base(context)
        {
            amidaMan = context.AmidaMan;
        }

        public override void Enter()
        {
            base.Enter();
            amidaMan.CastRayToForward();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}