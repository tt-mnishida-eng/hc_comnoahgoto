//  AmidaState.cs

using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using GoToYou.Detail.GameStage.Line;
using GoToYou.Detail.GameStage.People;
using UniRx;
using UnityEditor;

namespace GoToYou.Detail.GameStage.States
{
    /// <summary>
    /// 
    /// StateBehaviour
    /// </summary>
    public sealed class AmidaState : StateBase<GoToYouStage, GoToYouStates>
    {
        AmidaMan amidaMan;
        PersonInNeed personInNeed;

        IDisposable successSubscribe;
        IDisposable failSubscribe;


        int lineLayerMask;
        AmidaLine currentAmidaLine = null;
        float rayDepth = 60f;
        Vector3 mouseDownPosition;
        Camera currentCamera;

        IDisposable subscribe;

        ParticleSystem digParticle;

        public AmidaState(GoToYouStage context) : base(context)
        {
            lineLayerMask = LayerMask.GetMask("AmidaLine");

            amidaMan = context.AmidaMan;
            personInNeed = Context.PersonInNeed;
            currentCamera = Camera.main;
        }

        public override void Enter()
        {
            base.Enter();
            digParticle = Context.DigParticle;
            personInNeed.BeQuiet();
            failSubscribe = amidaMan.OnFail.Subscribe(x => Context.Fail());
            successSubscribe = amidaMan.OnSuccess.Subscribe(x => Context.Success());
            Context.StartCoroutine(DoStartAmida());
        }

        IEnumerator DoStartAmida()
        {
            yield return new WaitForSeconds(0.5f);
            amidaMan.CastRayToForward();

            yield break;
        }

        public override void Update()
        {
            base.Update();
            var inputMousePos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = currentCamera.ScreenPointToRay(inputMousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, rayDepth, lineLayerMask))
                {
                    Debug.Log(hit.collider.gameObject.transform.position);
                    var amidaInfo = hit.collider.gameObject.GetComponent<AmidaLineInformation>();
                    if (amidaInfo != null)
                    {
                        if (amidaInfo.LineType == AmidaLineType.Vertical)
                        {
                            mouseDownPosition =
                                currentCamera.ScreenToWorldPoint(
                                    new Vector3(inputMousePos.x, inputMousePos.z, rayDepth));
                            currentAmidaLine = Context.AddHorizonLine(amidaInfo.LineIndex);

                            subscribe = currentAmidaLine.OnDrawFinished.Subscribe(x => FinishDraw(x));
                            currentAmidaLine.Length = 0;
                            var vAmida = amidaInfo.AmidaLine;
                            currentAmidaLine.transform.position = new Vector3(vAmida.transform.position.x,
                                vAmida.transform.position.y, hit.point.z);

                            digParticle.Play();
                        }
                        else
                        {
                        }
                    }
                }

                Debug.DrawRay(ray.origin, ray.direction * 60, Color.red, 5);
            }
            else if (Input.GetMouseButton(0))
            {
                if (currentAmidaLine != null)
                {
                    var currentPosition = currentCamera.ScreenToWorldPoint(
                        new Vector3(inputMousePos.x, inputMousePos.z, rayDepth));
                    var diff = mouseDownPosition - currentPosition;
                    currentAmidaLine.transform.localEulerAngles = new Vector3(0, 90, 0);
                    currentAmidaLine.Length = diff.x;
                    var lineEndPos = currentAmidaLine.EndNode.transform.position;
                    var digPos = new Vector3(lineEndPos.x, 2, lineEndPos.z);
                    digParticle.transform.position = digPos;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (currentAmidaLine == null) return;
                currentAmidaLine = null;
                subscribe.Dispose();
                digParticle.Stop();
            }
        }

        void FinishDraw(Vector3 endlinePos)
        {
            digParticle.Stop();

            var diff = currentAmidaLine.transform.position - endlinePos;
            currentAmidaLine.Length = diff.x;
            currentAmidaLine = null;
            amidaMan.CastRayToForward();
            subscribe.Dispose();
        }

        public override void Exit()
        {
            base.Exit();
            successSubscribe.Dispose();
            failSubscribe.Dispose();
        }
    }
}