namespace GoToYou.Adapter.Repositories.Interfaces
{
    public interface IAnalyticEventSender
    {
        void SendGameStart(int stageNo);
        void SendLevelStart(int stageNo);
        void SendLevelEnd(int stageNo);
    }
}