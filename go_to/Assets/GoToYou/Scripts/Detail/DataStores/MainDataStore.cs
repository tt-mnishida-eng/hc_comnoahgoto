using GoToYou.Adapter.Repositories.Interfaces;
using GoToYou.Data.Entity;
using Nimitools.CA.Detail;
using UnityEngine;

namespace GoToYou.Detail.DataStores
{
    public class MainDataStore : DataStoreBase, IMainDataStore
    {
        UserEntity userEntity = new UserEntity();

        public void Initialize()
        {
        }

        public UserEntity GetUserEntity()
        {
            return userEntity;
        }
    }
}