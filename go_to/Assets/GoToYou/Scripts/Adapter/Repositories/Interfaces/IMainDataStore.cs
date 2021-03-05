using GoToYou.Data.Entity;

namespace GoToYou.Adapter.Repositories.Interfaces
{
    public interface IMainDataStore
    {
        UserEntity GetUserEntity();
    }
}