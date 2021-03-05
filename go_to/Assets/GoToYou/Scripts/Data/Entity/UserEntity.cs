using UnityEngine;

namespace GoToYou.Data.Entity
{
    public class UserEntity
    {
        [SerializeField] int userId;

        public int UserId => userId;

        [SerializeField] int currentStage;
        public int CurrentStage => this.currentStage;

        [SerializeField] int progress;
        public int Progress => this.progress;
        
    }
}