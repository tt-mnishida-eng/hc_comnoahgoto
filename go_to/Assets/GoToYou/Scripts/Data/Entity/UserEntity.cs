using UnityEngine;

namespace GoToYou.Data.Entity
{
    public class UserEntity
    {
        [SerializeField] int userId = 0;

        public int UserId => userId;


        [SerializeField] int progress = 0;

        public UserEntity()
        {
        }

        public int Progress
        {
            get { return PlayerPrefs.GetInt("Progress", 0); }
            set => PlayerPrefs.SetInt("Progress", value);
        }

        public int ChallengeProgress
        {
            get { return PlayerPrefs.GetInt("ChallengeProgress", -1); }
            set => PlayerPrefs.SetInt("ChallengeProgress", value);
        }

        public bool IsCurrentStageSuccess { get; set; } = false;
    }
}