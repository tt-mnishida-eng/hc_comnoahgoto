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
            foreach (var stage in stages)
            {
                if (stage.activeSelf)
                    stage.SetActive(false);
            }

            var len = stages.Length;
            var stageIndex = progress % len;
            var rootTransform = stages[stageIndex].transform;
            rootTransform.gameObject.SetActive(true);
            foreach (Transform stageChild in rootTransform)
            {
                Debug.Log(stageChild.tag);
                if (stageChild.CompareTag("HorizonLineContainer"))
                {
                    currentHorizonLineContainer = stageChild.gameObject;
                }

                if (stageChild.CompareTag("VerticalLineContainer"))
                {
                    currentVerticalLineContainer = stageChild.gameObject;
                }
            }
        }
    }
}