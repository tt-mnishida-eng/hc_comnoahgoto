using System;
using Common.Extension;
using UniRx;
using Unity.QuickSearch;
using UnityEngine;

namespace GoToYou.Detail.GameStage.Line
{
    public class AmidaLine : MonoBehaviour
    {
        [SerializeField] AmidaLineInformation lineInfo;
        public int LineIndex => lineInfo.LineIndex;
        public AmidaLineType LineType => lineInfo.LineType;

        [SerializeField] Transform lineTransform;

        public float Length
        {
            get => lineTransform.localScale.z;
            set
            {
                lineTransform.localScale =
                    new Vector3(lineTransform.localScale.x, lineTransform.localScale.y, value);
                endNode.transform.localPosition =
                    new Vector3(endNode.transform.localPosition.x, endNode.transform.localPosition.y, -value);
            }
        }

        [SerializeField] BoxCollider groundCollider;

        [SerializeField] AmidaNode beginNode;
        [SerializeField] AmidaNode endNode;


        Subject<Unit> drawFinishedSubject = new Subject<Unit>();
        public IObservable<Unit> OnDrawFinished => drawFinishedSubject;

        public void Initialize(int lineIndex, AmidaLineType lineType)
        {
            lineInfo.LineType = lineType;
            lineInfo.LineIndex = lineIndex;
            beginNode.IsCrossEndNode = false;
            beginNode.OnStartCross.Subscribe(StartCross);
            beginNode.OnEndCross.Subscribe(EndCross);

            endNode.IsCrossEndNode = false;
            endNode.OnStartCross.Subscribe(StartCross);
            endNode.OnEndCross.Subscribe(EndCross);
        }

        public void StartCross(AmidaNode node)
        {
            if (!beginNode.IsCrossEndNode && !endNode.IsCrossEndNode)
            {
                if (beginNode.VerticalLineIndex == node.VerticalLineIndex)
                {
                    endNode.IsCrossEndNode = true;
                }
                else
                {
                    beginNode.IsCrossEndNode = true;
                }
            }
        }

        public void EndCross(AmidaNode node)
        {
            if (node.IsCrossEndNode)
                SetAmidaNodeCollidersEnabled(false);
        }

        public void SetBeginNode(int verticalIndex, int horizonIndex)
        {
            beginNode.VerticalLineIndex = verticalIndex;
            beginNode.HorizonLineIndex = horizonIndex;
        }

        public void SetEndNode(int verticalIndex, int horizonIndex)
        {
            endNode.VerticalLineIndex = verticalIndex;
            endNode.HorizonLineIndex = horizonIndex;
        }

        public void SetAmidaNodeCollidersEnabled(bool enabled)
        {
            beginNode.BoxCollider.enabled = enabled;
            endNode.BoxCollider.enabled = enabled;
        }

        public void SetGroundColliderEnabled(bool enabled)
        {
            groundCollider.enabled = enabled;
        }

        void OnTriggerEnter(Collider other)
        {
            if (LineType == AmidaLineType.Horizon)
            {
                var gObj = other.gameObject;
                if (gObj != null)
                {
                    var amidaInfo = gObj.GetComponent<AmidaLineInformation>();
                    if (amidaInfo != null)
                    {
                        if (amidaInfo.LineType == AmidaLineType.Vertical)
                        {
                            if (amidaInfo.LineIndex != beginNode.VerticalLineIndex)
                            {
                                SetEndNode(amidaInfo.LineIndex, LineIndex);
                                SetAmidaNodeCollidersEnabled(true);
                                drawFinishedSubject.OnNext(Unit.Default);
                            }
                        }
                    }
                }
            }
        }
    }
}