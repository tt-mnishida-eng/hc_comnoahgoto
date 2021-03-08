//  DrawState.cs
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GoToYou.Detail.GameStage.States
{
    /// <summary>
    /// 
        /// StateBehaviour
    /// </summary>
    public sealed class DrawState : StateBase<GoToYouStage, GoToYouStates>
    {
        public DrawState(GoToYouStage context) : base(context) { }
    
        public override void Enter( )
        {
            base.Enter();
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
