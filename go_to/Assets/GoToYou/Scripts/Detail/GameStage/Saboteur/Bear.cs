using System.Collections;
using GoToYou.Detail.GameStage.People;
using GoToYou.Detail.GameStage.Utility;
using UniRx;
using UnityEngine;

namespace GoToYou.Detail.GameStage.Saboteur
{
    public class Bear : SaboteurBase, IMovable
    {
        [SerializeField] protected Animator animator;

        [SerializeField] Transform[] TargetPointsForMove;
        [SerializeField] float duration = 2;
        [SerializeField] float idlingSeconds = 1;
        LoopMovingGear movingGear;

        void Start()
        {
            var len = TargetPointsForMove.Length;
            if (len > 0)
            {
                var positions = new Vector3[len];
                for (var i = 0; i < len; i++)
                {
                    positions[i] = TargetPointsForMove[i].position;
                }

                movingGear = new LoopMovingGear(transform, positions);
                movingGear.Duration = duration;
                movingGear.IdlingSeconds = idlingSeconds;
                movingGear.OnIdle.Subscribe(x => Idle()).AddTo(this);
                movingGear.OnMove.Subscribe(x => Walk()).AddTo(this);


                StartMove();
            }
        }

        void Idle()
        {
            Play(AgentAnimatorParameters.Idle);
        }

        void Walk()
        {
            Play(AgentAnimatorParameters.Walk);
        }

        public void StartMove()
        {
            movingGear.Drive();
        }

        public virtual void Play(string animationName)
        {
            animator.speed = 1;
            animator.SetTrigger(animationName);
        }


        protected override IEnumerator TouchAmidaMan(AmidaMan amidaMan, float fromReactionSec = 0)
        {
            var pos = new Vector3(amidaMan.transform.position.x, transform.position.y, amidaMan.transform.position.z);
            transform.LookAt(pos);
            Play(AgentAnimatorParameters.Attack);
            if (movingGear != null) movingGear.Stop();
            return base.TouchAmidaMan(amidaMan, fromReactionSec);
        }
    }
}