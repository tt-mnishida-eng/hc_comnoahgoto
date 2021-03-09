using UnityEngine;

namespace GoToYou.Data.Entity
{
    public class UserEntity
    {
        [SerializeField] int userId = 0;

        public int UserId => userId;


        [SerializeField] int progress = 0;
        public int Progress => this.progress;


        public bool IsCurrentStageSuccess { get; set; } = false;
    }
}