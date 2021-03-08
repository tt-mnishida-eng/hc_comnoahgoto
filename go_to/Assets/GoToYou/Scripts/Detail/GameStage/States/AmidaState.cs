//  AmidaState.cs

using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
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
            Context.StartCoroutine(DoStartAmida());
        }

        IEnumerator DoStartAmida()
        {
            yield return new WaitForSeconds(0.5f);
            amidaMan.CastRayToForward();

            yield break;
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