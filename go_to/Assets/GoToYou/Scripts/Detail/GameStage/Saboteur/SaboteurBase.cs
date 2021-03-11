using System;
using System.Collections;
using GoToYou.Detail.GameStage.People;
using UnityEngine;

namespace GoToYou.Detail.GameStage.Saboteur
{
    public class SaboteurBase : MonoBehaviour, ISaboteur
    {
        Vector3 defaultPosition;

        protected virtual void Start()
        {
            defaultPosition = transform.position;
        }

        public virtual void Reset()
        {
            transform.position = defaultPosition;
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            var gObj = other.gameObject;
            if (gObj != null && gObj.CompareTag("AmidaMan"))
            {
                var amidaMan = gObj.GetComponent<AmidaMan>();
                StartCoroutine(TouchAmidaMan(amidaMan));
            }
        }

        protected virtual IEnumerator TouchAmidaMan(AmidaMan amidaMan, float fromReactionSec = 0)
        {
            if (Mathf.Approximately(fromReactionSec, 0))
            {
                amidaMan.FallBack();
            }
            else
            {
                yield return new WaitForSeconds(fromReactionSec);
                amidaMan.FallBack();
            }

            yield break;
        }

        protected void OnTriggerEnter(Collider other)
        {
            var gObj = other.gameObject;
            if (gObj != null && gObj.CompareTag("AmidaMan"))
            {
            }
        }
    }
}