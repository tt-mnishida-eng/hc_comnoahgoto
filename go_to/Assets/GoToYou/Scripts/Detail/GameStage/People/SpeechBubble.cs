using System;
using Common.Extension;
using DG.Tweening;
using UnityEngine;

namespace GoToYou.Detail.GameStage.People
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField] GameObject bubble;

        void Start()
        {
            bubble.transform.localScale = Vector3.one;
        }

        public void Appear()
        {
            var seq = DOTween.Sequence();
            // seq.Append(bubble.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InCubic));
            seq.Append(bubble.transform.DOShakeRotation(0.5f, 5f));
            seq.SetLoops(-1);
            seq.Play();
        }

        public void Disappear()
        {
            // this.SetActive(false);
            var seq = DOTween.Sequence();
            seq.Append(bubble.transform.DOScale(Vector3.zero, 0.1f));
        }
    }
}