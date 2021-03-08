using System;
using Common.Actor;
using UnityEngine;

namespace GoToYou.Detail.GameStage.People
{
    public class AmidaMan : Actor
    {
        enum States
        {
            Idle = 0,
            Walk,
        }

        States currentState = States.Idle;
        int animaNodeLayerMask = 0;

        int laneIndex = 0;

        float speed = 5f;

        Vector3 targetPosition;

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
            float distance = 5f;
            if (Physics.Raycast(ray, out hit, distance, animaNodeLayerMask))
            {
                targetPosition = hit.collider.transform.position;
                targetPosition.y = 0;
                transform.LookAt(targetPosition);
                Walk();
                return true;
            }


            Debug.DrawRay(from, direction * distance, Color.red, 3);

            direction = Vector3.left;
            ray = new Ray(from, direction);

            if (Physics.Raycast(ray, out hit, distance, animaNodeLayerMask))
            {
                targetPosition = hit.collider.transform.position;
                targetPosition.y = 0;
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
                targetPosition = hit.collider.transform.position;
                targetPosition.y = 0;
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
            Play(AmidaManAnimetorParameters.Dance);
        }

        public void Walk()
        {
            currentState = States.Walk;
            Play(AmidaManAnimetorParameters.Walk);
        }

        public void FallBack()
        {
            Play(AmidaManAnimetorParameters.FallBack);
        }
    }
}