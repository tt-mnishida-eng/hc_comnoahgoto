using System;
using GoToYou.Detail.GameStage.People;
using UnityEngine;

namespace GoToYou.Detail.GameStage.Saboteur
{
    public class SaboteurBase : MonoBehaviour
    {
        protected virtual void OnCollisionEnter(Collision other)
        {
            var gObj = other.gameObject;
            if (gObj != null && gObj.CompareTag("AmidaMan"))
            {
                var amidaMan = gObj.GetComponent<AmidaMan>();
                amidaMan.FallBack();
            }
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