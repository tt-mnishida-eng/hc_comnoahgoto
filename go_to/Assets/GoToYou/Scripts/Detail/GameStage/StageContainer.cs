using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using GoToYou.Detail.GameStage.Saboteur;
using UnityEngine;

namespace GoToYou.Detail.GameStage
{
    public class StageContainer : MonoBehaviour
    {
        [SerializeField] GameObject[] stages;

        [SerializeField] Vector3 firstPosition;
        public Vector3 FirstPosition => firstPosition;
        GameObject currentHorizonLineContainer;
        public GameObject CurrentHorizonLineContainer => currentHorizonLineContainer;

        GameObject currentVerticalLineContainer;
        public GameObject CurrentVerticalLineContainer => currentVerticalLineContainer;

        List<ISaboteur> saboteurs = new List<ISaboteur>();
        List<IMovable> movables = new List<IMovable>();

        public void Reset()
        {
            foreach (var saboteur in saboteurs)
            {
                saboteur.Reset();
            }
        }

        public void SetProgress(int progress)
        {
            currentVerticalLineContainer = null;
            currentHorizonLineContainer = null;
            saboteurs.Clear();
            movables.Clear();
            foreach (var stage in stages)
            {
                if (stage.activeSelf)
                    stage.SetActive(false);
            }

            var len = stages.Length;
            var stageIndex = progress % len;
            // var stageIndex = 5;
            var rootTransform = stages[stageIndex].transform;
            rootTransform.gameObject.SetActive(true);
            foreach (Transform stageChild in rootTransform)
            {
                if (stageChild.CompareTag("HorizonLineContainer"))
                {
                    currentHorizonLineContainer = stageChild.gameObject;
                }

                if (stageChild.CompareTag("VerticalLineContainer"))
                {
                    currentVerticalLineContainer = stageChild.gameObject;
                }

                if (stageChild.CompareTag("FirstPosition"))
                {
                    firstPosition = stageChild.position;
                }

                var saboteur = stageChild.GetComponent<ISaboteur>();
                if (saboteur != null)
                {
                    saboteurs.Add(saboteur);
                    var movable = stageChild.GetComponent<IMovable>();
                    if (movable != null)
                        movables.Add(movable);
                }
            }
        }

        public void StartMove()
        {
            foreach (var movable in movables)
            {
                movable.StartMove();
            }
        }
    }
}