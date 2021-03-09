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


        Subject<Vector3> drawFinishedSubject = new Subject<Vector3>();
        public IObservable<Vector3> OnDrawFinished => drawFinishedSubject;

        public void Initialize(int lineIndex, AmidaLineType lineType)
        {
            lineInfo.LineType = lineType;
            lineInfo.LineIndex = lineIndex;
            beginNode.OnEndCross.Subscribe(EndCross);

            endNode.OnEndCross.Subscribe(EndCross);
        }


        void EndCross(AmidaNode node)
        {
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
                                drawFinishedSubject.OnNext(amidaInfo.AmidaLine.transform.position);
                            }
                        }
                    }
                }
            }
        }
    }
}