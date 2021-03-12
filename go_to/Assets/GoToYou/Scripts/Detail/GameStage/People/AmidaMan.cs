using System;
using Common.Actor;
using DG.Tweening;
using GoToYou.Detail.GameStage.Line;
using GoToYou.Detail.GameStage.Saboteur;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GoToYou.Detail.GameStage.People
{
    public class AmidaMan : Actor
    {
        AmidaNode previousNode = null;
        AmidaNode currentNode = null;

        bool isSideMoving = false;

        enum States
        {
            Idle = 0,
            Walk,
            Run,
            Dance,
            FallBack,
        }

        States currentState = States.Idle;
        int animaNodeLayerMask = 0;
        int saboteurLayerMask = 0;

        int laneIndex = 0;

        float speed = 5f;

        Vector3 firstPosition;

        public Vector3 FirstPositeion
        {
            get => firstPosition;
            set
            {
                firstPosition = value;
                targetPosition = value;
                transform.position = value;
            }
        }

        Vector3 targetPosition;
        [SerializeField] ParticleSystem bloodParticle;
        [SerializeField] PersonInNeed personInNeed;

        Subject<Unit> successSubject = new Subject<Unit>();
        public IObservable<Unit> OnSuccess => successSubject;
        Subject<Unit> failureSubject = new Subject<Unit>();
        public IObservable<Unit> OnFail => failureSubject;

        void Awake()
        {
            bloodParticle.Stop();
            bloodParticle.gameObject.SetActive(false);
        }

        protected override void Start()
        {
            base.Start();
            animaNodeLayerMask = LayerMask.GetMask("AmidaNode");
            saboteurLayerMask = LayerMask.GetMask("Saboteur");
            targetPosition = transform.position;
            firstPosition = transform.position;
            Idle();
        }

        public void Reset()
        {
            transform.position = firstPosition;
            bloodParticle.Stop();
            bloodParticle.gameObject.SetActive(false);
            Idle();
        }

        void Update()
        {
            if (currentState == States.Run)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                if (Vector3.Distance(targetPosition, transform.position) < 0.01f)
                {
                    FinishMoving();
                }
            }
        }

        void FinishMoving()
        {
            Debug.Log("FinishMoving");
            isSideMoving = false;
            if (currentNode.HorizonLineIndex < 0)
            {
                if (currentNode.HorizonLineIndex == -1)
                    Dance();
                return;
            }

            if (previousNode != null && previousNode.HorizonLineIndex == currentNode.HorizonLineIndex)
            {
                currentNode.TriggerEndCross();
            }

            previousNode = currentNode;
            currentNode = null;
            if (CastRayToSide())
            {
            }
            else if (CastRayToForward())
            {
            }
            else
            {
                Idle();
            }
        }

        bool CastRayToSide()
        {
            var from = transform.position;
            var direction = Vector3.right;
            var ray = new Ray(from, direction);
            RaycastHit hit;
            float distance = 7f;
            if (Physics.Raycast(ray, out hit, distance, animaNodeLayerMask))
            {
                currentNode = hit.collider.gameObject.GetComponent<AmidaNode>();
                if (!currentNode.CanCross)
                    return false;
                // ray = new Ray(from + Vector3.up, direction);
                // if (Physics.Raycast(ray, out hit, distance - 1.2f, saboteurLayerMask) &&
                //     hit.collider.gameObject.GetComponent<IMovable>() != null)
                // {
                //     Debug.Log(hit.collider.gameObject.name);
                // }
                // else
                {
                    targetPosition = hit.collider.transform.position;
                    currentNode.TriggerStartCross();

                    transform.LookAt(targetPosition);
                    isSideMoving = true;
                    Run();
                    return true;
                }
            }


            Debug.DrawRay(from, direction * distance, Color.red, 3);

            direction = Vector3.left;
            ray = new Ray(from, direction);

            if (Physics.Raycast(ray, out hit, distance, animaNodeLayerMask))
            {
                currentNode = hit.collider.gameObject.GetComponent<AmidaNode>();

                if (!currentNode.CanCross)
                    return false;

                // ray = new Ray(from + Vector3.up, direction);
                // if (Physics.Raycast(ray, out hit, distance - 1.2f, saboteurLayerMask) &&
                //     hit.collider.gameObject.GetComponent<IMovable>() != null)
                // {
                //     Debug.Log(hit.collider.gameObject.name);
                // }
                // else
                {
                    targetPosition = hit.collider.transform.position;
                    currentNode.TriggerStartCross();
                    transform.LookAt(targetPosition);
                    isSideMoving = true;
                    Run();
                    return true;
                }
            }

            Debug.DrawRay(from, direction * distance, Color.red, 3);
            return false;
        }

        public bool CastRayToForward()
        {
            if (isSideMoving) return false;
            var from = transform.position;
            from.z += 0.3f;
            var direction = Vector3.back;
            var ray = new Ray(from, direction);
            RaycastHit hit;
            float distance = 20f;
            if (Physics.Raycast(ray, out hit, distance, animaNodeLayerMask))
            {
                Debug.Log(hit.collider.name);
                currentNode = hit.collider.gameObject.GetComponent<AmidaNode>();
                currentNode.TriggerStartCross();
                targetPosition = hit.collider.transform.position;
                // targetPosition.y = 0;
                transform.LookAt(targetPosition);
                Run();
                return true;
            }

            Debug.DrawRay(from, direction * distance, Color.red, 3);
            return false;
        }

        public void Idle()
        {
            currentState = States.Idle;
            Play(AmidaManAnimatorParameters.Idle);
        }

        public void Dance()
        {
            Vibration.VibrateNope();
            successSubject.OnNext(Unit.Default);
            currentState = States.Dance;
            var seq = DOTween.Sequence();
            seq.Append(transform.DOMove(personInNeed.transform.position - Vector3.left, 0.5f));
            seq.OnComplete(() => { Play(AmidaManAnimatorParameters.Dance); });
        }

        public void Walk()
        {
            currentState = States.Walk;
            Play(AmidaManAnimatorParameters.Walk);
        }

        public void Run()
        {
            currentState = States.Run;
            Play(AmidaManAnimatorParameters.Run);
        }

        public void FallBack()
        {
            Vibration.VibratePeek();
            failureSubject.OnNext(Unit.Default);
            bloodParticle.gameObject.SetActive(true);
            bloodParticle.Play();
            currentState = States.FallBack;
            Play(AmidaManAnimatorParameters.FallBack);
        }
    }
}