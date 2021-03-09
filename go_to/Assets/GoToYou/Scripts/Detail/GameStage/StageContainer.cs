using GameAnalyticsSDK.Setup;
using UnityEngine;

namespace GoToYou.Detail.GameStage
{
    public class StageContainer : MonoBehaviour
    {
        [SerializeField] GameObject[] stages;

        GameObject currentHorizonLineContainer;
        public GameObject CurrentHorizonLineContainer => currentHorizonLineContainer;

        GameObject currentVerticalLineContainer;
        public GameObject CurrentVerticalLineContainer => currentVerticalLineContainer;

        public void SetProgress(int progress)
        {
            var len = stages.Length;
            var stageIndex = progress % len;
            var rootTransform = stages[stageIndex].transform;
            foreach (Transform stageChild in rootTransform)
            {
                Debug.Log(stageChild.tag);
                if (stageChild.CompareTag("HorizonLineContainer"))
                {
                }

                if (stageChild.CompareTag("VerticalLineContainer"))
                {
                }
            }
        }
    }
}