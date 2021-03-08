using UnityEngine;

namespace GoToYou.Detail.GameStage.Road
{
    public class VerticalRoad : MonoBehaviour
    {
        [SerializeField] int laneIndex = 0;

        public int LaneIndex => laneIndex;
    }
}