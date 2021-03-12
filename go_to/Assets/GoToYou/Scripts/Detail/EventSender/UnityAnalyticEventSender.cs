using System.Collections.Generic;
using GoToYou.Adapter.Repositories.Interfaces;
using UnityEngine;
using UnityEngine.Analytics;

namespace GoToYou.Detail.EventSender
{
    public class UnityAnalyticEventSender : IAnalyticEventSender
    {
        public void SendGameStart(int stageNo)
        {
            AnalyticsEvent.Custom("game_start", new Dictionary<string, object>
            {
                {"stage_no", stageNo}
            });
        }

        public void SendLevelStart(int stageNo)
        {
            AnalyticsEvent.Custom("level_start", new Dictionary<string, object>
            {
                {"stage_no", stageNo}
            });
        }

        public void SendLevelEnd(int stageNo)
        {
            AnalyticsEvent.Custom("level_end", new Dictionary<string, object>
            {
                {"stage_no", stageNo}
            });
        }
    }
}