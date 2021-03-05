using GoToYou.Data.Entity;

namespace GoToYou.Domain
{
    public interface IMainRepository
    {
        UserEntity GetUserEntity();
    }
}