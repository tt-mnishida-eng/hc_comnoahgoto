using UnityEngine;
using DG.Tweening;
using System;

namespace Common
{
    public class MoveAction
    {
        Transform transform;

        public Transform Transform
        {
            set => transform = value;
        }

        Transform[] route;
        int maxStep = 0;
        Sequence sequence;
        Action OnEnd { get; set; }

        public bool IsWithRotation { get; set; } = false;
        public float BeginInterval { get; set; } = 0;

        public float EndInterval { get; set; } = 0;


        public void Begin(Action callback, float duration = 1)
        {
            OnEnd = callback;
            maxStep = route.Length;
            sequence = DOTween.Sequence();
            sequence.AppendInterval(BeginInterval);
            for (var i = 0; i < maxStep; i++)
            {
                if (IsWithRotation)
                {
                    var dur = duration / maxStep;
                    var seq = DOTween.Sequence();
                    seq.Join(transform.DOMove(route[i].position, dur));
                    seq.Join(transform.DOLocalRotateQuaternion(route[i].rotation, dur));
                    sequence.Append(seq);
                }
                else
                    sequence.Append(transform.DOMove(route[i].position, duration / maxStep));
            }

            sequence.AppendInterval(EndInterval);
            sequence.OnComplete(End);
        }

        public void Begin(Rigidbody rb, Action callback, float duration = 1)
        {
            OnEnd = callback;
            maxStep = route.Length;
            sequence = DOTween.Sequence();
            sequence.AppendInterval(BeginInterval);
            for (var i = 0; i < maxStep; i++)
            {
                if (IsWithRotation)
                {
                    var dur = duration / maxStep;
                    var seq = DOTween.Sequence();
                    seq.Join(rb.DOMove(route[i].position, dur));
                    seq.Join(rb.DORotate(route[i].eulerAngles, dur));
                    sequence.Append(seq);
                }
                else
                    sequence.Append(rb.DOMove(route[i].position, duration / maxStep));
            }

            sequence.AppendInterval(EndInterval);
            sequence.OnComplete(End);
        }

        void End()
        {
            OnEnd?.Invoke();
        }
    }
}