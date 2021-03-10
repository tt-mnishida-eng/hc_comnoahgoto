//  SuccessState.cs

using System.Collections.Generic;
using UnityEngine;
using System;
using GoToYou.Detail.GameStage.People;

namespace GoToYou.Detail.GameStage.States
{
    /// <summary>
    /// 
    /// StateBehaviour
    /// </summary>
    public sealed class SuccessState : StateBase<GoToYouStage, GoToYouStates>
    {
        PersonInNeed personInNeed;

        public SuccessState(GoToYouStage context) : base(context)
        {
        }

        public override void Enter()
        {
            base.Enter();
            personInNeed = Context.PersonInNeed;
            personInNeed.Play(PersonInNeedAnimetorParameters.Dance);
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