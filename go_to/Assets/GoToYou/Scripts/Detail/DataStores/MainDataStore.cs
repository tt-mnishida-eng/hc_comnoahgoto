using GoToYou.Adapter.Repositories.Interfaces;
using GoToYou.Data.Entity;
using Nimitools.CA.Detail;
using UnityEngine;

namespace GoToYou.Detail.DataStores
{
    public class MainDataStore : DataStoreBase, IMainDataStore
    {
        UserEntity userEntity;

        public void Initialize()
        {
            userEntity = new UserEntity();
        }

        public UserEntity GetUserEntity()
        {
            return userEntity;
        }
    }
}