using System;
using UnityEngine;

namespace GoToYou.Detail.GameStage.Saboteur
{
    public class Bomb : SaboteurBase
    {
        [SerializeField] ParticleSystem bombParticle;

        void Awake()
        {
            bombParticle.Stop();
            bombParticle.gameObject.SetActive(false);
        }

        void PlayBomb()
        {
            bombParticle.gameObject.SetActive(true);
            bombParticle.Play();
        }

        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);
            var gObj = other.gameObject;
            if (gObj != null && gObj.CompareTag("AmidaMan"))
            {
                PlayBomb();
            }
        }
    }
}