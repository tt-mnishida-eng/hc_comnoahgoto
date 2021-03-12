using GoToYou.Data.Entity;

namespace GoToYou.Domain
{
    public interface IMainRepository
    {
        UserEntity GetUserEntity();
        void SendGameStartEvent();

        void SendLevelStartEvent();

        void SendLevelEndEvent();
    }
}