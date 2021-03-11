using System;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace GoToYou.Detail.GameStage.Utility
{
    public class LoopMovingGear
    {
        Transform body;
        Vector3[] positions;

        int currentIndex = 0;
        bool isIncrease = true;
        public float IdlingSeconds { get; set; } = 1;
        Sequence moveSequence;

        Subject<Unit> moveSubject = new Subject<Unit>();
        public IObservable<Unit> OnMove => moveSubject;

        Subject<Unit> idleSubject = new Subject<Unit>();
        public IObservable<Unit> OnIdle => idleSubject;
        public float Duration { get; set; } = 1;

        public LoopMovingGear(Transform body, Vector3[] positions)
        {
            this.body = body;
            this.positions = positions;
        }

        public void Drive()
        {
            Move();
        }

        void Move()
        {
            var pos = positions[currentIndex];
            if (Vector3.Distance(body.position, pos) < 0.01f)
            {
                FinishMove();
                return;
            }

            moveSubject.OnNext(Unit.Default);
            body.LookAt(pos);
            moveSequence = DOTween.Sequence();
            moveSequence.Append(body.DOMove(pos, Duration).SetEase(Ease.Linear));
            moveSequence.OnComplete(FinishMove);
        }

        void FinishMove()
        {
            idleSubject.OnNext(Unit.Default);


            var len = positions.Length;
            if (isIncrease)
            {
                if (currentIndex >= len - 1)
                {
                    isIncrease = false;
                    currentIndex--;
                }
                else
                {
                    currentIndex++;
                }
            }
            else
            {
                if (currentIndex <= 0)
                {
                    isIncrease = true;
                    currentIndex++;
                }
                else
                {
                    currentIndex--;
                }
            }

            moveSequence = DOTween.Sequence();
            moveSequence.AppendInterval(IdlingSeconds);
            moveSequence.OnComplete(Move);
        }

        public void Stop()
        {
            if (moveSequence != null)
                moveSequence.Kill();
            moveSequence = null;
        }
    }
}