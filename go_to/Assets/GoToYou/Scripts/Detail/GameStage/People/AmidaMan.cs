using System;
using Common.Actor;
using GoToYou.Detail.GameStage.Line;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GoToYou.Detail.GameStage.People
{
    public class AmidaMan : Actor
    {
        AmidaNode previousNode = null;
        AmidaNode currentNode = null;

        enum States
        {
            Idle = 0,
            Walk,
            Dance,
            FallBack
        }

        States currentState = States.Idle;
        int animaNodeLayerMask = 0;

        int laneIndex = 0;

        float speed = 5f;

        Vector3 targetPosition;
        [SerializeField] ParticleSystem bloodParticle;


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
            targetPosition = transform.position;
            Idle();
        }

        void Update()
        {
            if (currentState == States.Walk)
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
            if (currentNode.HorizonLineIndex < 0)
            {
                successSubject.OnNext(Unit.Default);
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
                currentNode.TriggerStartCross();

                targetPosition = hit.collider.transform.position;
                // targetPosition.y = 0
                transform.LookAt(targetPosition);
                Walk();
                return true;
            }


            Debug.DrawRay(from, direction * distance, Color.red, 3);

            direction = Vector3.left;
            ray = new Ray(from, direction);

            if (Physics.Raycast(ray, out hit, distance, animaNodeLayerMask))
            {
                currentNode = hit.collider.gameObject.GetComponent<AmidaNode>();
                currentNode.TriggerStartCross();
                targetPosition = hit.collider.transform.position;
                // targetPosition.y = 0;
                transform.LookAt(targetPosition);
                Walk();
                return true;
            }

            Debug.DrawRay(from, direction * distance, Color.red, 3);
            return false;
        }

        public bool CastRayToForward()
        {
            var from = transform.position;
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
                Walk();
                return true;
            }

            Debug.DrawRay(from, direction * distance, Color.red, 3);
            return false;
        }

        public void Idle()
        {
            currentState = States.Idle;
            Play(AmidaManAnimetorParameters.Idle);
        }

        public void Dance()
        {
            currentState = States.Dance;
            Play(AmidaManAnimetorParameters.Dance);
        }

        public void Walk()
        {
            currentState = States.Walk;
            Play(AmidaManAnimetorParameters.Walk);
        }

        public void FallBack()
        {
            bloodParticle.gameObject.SetActive(true);
            bloodParticle.Play();
            currentState = States.FallBack;
            Play(AmidaManAnimetorParameters.FallBack);
        }
    }
}