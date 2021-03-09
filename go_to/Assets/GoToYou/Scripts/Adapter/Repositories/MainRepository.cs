using GoToYou.Adapter.Repositories.Interfaces;
using GoToYou.Data.Entity;
using GoToYou.Domain;
using Nimitools.CA.Domain;

namespace GoToYou.Adapter.Repositories
{
    public class MainRepository : IMainRepository
    {
        IMainDataStore dataStore;

        public MainRepository(IMainDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public UserEntity GetUserEntity()
        {
            return dataStore.GetUserEntity();
        }
    }
}