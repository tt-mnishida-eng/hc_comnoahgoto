using Common.Actor;
using UnityEngine;

namespace GoToYou.Detail.GameStage.People
{
    public class PersonInNeed : Actor
    {
        [SerializeField] SpeechBubble speechBubble;

        public void Idle()
        {
            Play(PersonInNeedAnimatorParameters.Idle);
        }

        public void Dance()
        {
            Play(PersonInNeedAnimatorParameters.Dance);
        }

        public void Walk()
        {
            Play(PersonInNeedAnimatorParameters.Walk);
        }

        public void Say()
        {
            speechBubble.Appear();
        }

        public void BeQuiet()
        {
            speechBubble.Disappear();
        }
    }
}