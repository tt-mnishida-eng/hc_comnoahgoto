using Common.Actor;
using UnityEngine;

namespace GoToYou.Detail.GameStage.People
{
    public class PersonInNeed : Actor
    {
        public void Idle()
        {
            Play(PersonInNeedAnimetorParameters.Idle);
        }

        public void Dance()
        {
            Play(PersonInNeedAnimetorParameters.Dance);
        }

        public void Walk()
        {
            Play(PersonInNeedAnimetorParameters.Walk);
        }
    }
}