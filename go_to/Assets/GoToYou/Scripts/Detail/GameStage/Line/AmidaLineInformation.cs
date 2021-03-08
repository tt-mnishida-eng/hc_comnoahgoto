using UnityEngine;

namespace GoToYou.Detail.GameStage.Line
{
    public class AmidaLineInformation : MonoBehaviour
    {
        [SerializeField] AmidaLine amidaLine;

        public AmidaLine AmidaLine => amidaLine;

        [SerializeField] AmidaLineType lineType;

        public AmidaLineType LineType
        {
            get => lineType;
            set => lineType = value;
        }


        [SerializeField] int lineIndex;

        public int LineIndex
        {
            get => lineIndex;
            set => lineIndex = value;
        }
    }
}