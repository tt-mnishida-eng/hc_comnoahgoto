//  WaitDrawState.cs

using System.Collections.Generic;
using UnityEngine;
using System;
using GoToYou.Detail.GameStage.Line;
using UniRx;

namespace GoToYou.Detail.GameStage.States
{
    /// <summary>
    /// 
    /// StateBehaviour
    /// </summary>
    public sealed class WaitDrawState : StateBase<GoToYouStage, GoToYouStates>
    {
        int lineLayerMask;
        AmidaLine currentAmidaLine = null;
        float rayDepth = 60f;
        Vector3 mouseDownPosition;
        Camera currentCamera;

        IDisposable subscribe;

        public WaitDrawState(GoToYouStage context) : base(context)
        {
            lineLayerMask = LayerMask.GetMask("AmidaLine");
        }

        public override void Enter()
        {
            base.Enter();
            currentCamera = Camera.main;
        }

        public override void Update()
        {
            base.Update();
            var inputMousePos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(inputMousePos);
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

                            subscribe = currentAmidaLine.OnDrawFinished.Subscribe(x => FinishDraw());
                            currentAmidaLine.Length = 0;
                            var vAmida = amidaInfo.AmidaLine;
                            currentAmidaLine.transform.position = new Vector3(vAmida.transform.position.x,
                                vAmida.transform.position.y, hit.point.z);
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
                    Debug.Log(diff);

                    currentAmidaLine.transform.localEulerAngles = new Vector3(0, 90, 0);
                    currentAmidaLine.Length = diff.x;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (currentAmidaLine == null) return;
                currentAmidaLine = null;
                subscribe.Dispose();
            }
        }

        void FinishDraw()
        {
            currentAmidaLine = null;
            subscribe.Dispose();
            Context.StartAmida();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}