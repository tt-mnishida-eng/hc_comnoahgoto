using GoToYou.Adapter.Repositories.Interfaces;
using GoToYou.Data.Entity;
using GoToYou.Domain;
using Nimitools.CA.Domain;

namespace GoToYou.Adapter.Repositories
{
    public class MainRepository : IMainRepository
    {
        IMainDataStore dataStore;
        IAnalyticEventSender eventSender;

        public MainRepository(IMainDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public void SetEventSender(IAnalyticEventSender eventSender)
        {
            this.eventSender = eventSender;
        }

        public UserEntity GetUserEntity()
        {
            return dataStore.GetUserEntity();
        }

        public void SendGameStartEvent()
        {
            var userEntity = dataStore.GetUserEntity();
            eventSender.SendGameStart(userEntity.Progress + 1);
        }

        public void SendLevelStartEvent()
        {
            var userEntity = dataStore.GetUserEntity();
            if (userEntity.ChallengeProgress < userEntity.Progress)
            {
                userEntity.ChallengeProgress = userEntity.Progress;
                eventSender.SendLevelStart(userEntity.Progress + 1);
            }
        }

        public void SendLevelEndEvent()
        {
            var userEntity = dataStore.GetUserEntity();
            eventSender.SendLevelEnd(userEntity.Progress + 1);
        }
    }
}