using System;
using UnityEngine;

namespace GoToYou.Detail.GameStage.Saboteur
{
    public class Bomb : SaboteurBase
    {
        [SerializeField] ParticleSystem fuseParticle;
        [SerializeField] ParticleSystem bombParticle;
        MeshRenderer meshRenderer;

        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            bombParticle.Stop();
            bombParticle.gameObject.SetActive(false);
        }

        public override void Reset()
        {
            base.Reset();
            bombParticle.Stop();
            bombParticle.gameObject.SetActive(false);
            meshRenderer.enabled = false;
            fuseParticle.gameObject.SetActive(false);
        }

        void PlayBomb()
        {
            bombParticle.gameObject.SetActive(true);
            bombParticle.Play();
            meshRenderer.enabled = false;
            fuseParticle.gameObject.SetActive(false);
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