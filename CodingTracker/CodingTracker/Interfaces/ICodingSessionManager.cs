namespace CodingTracker
{
    internal interface ICodingSessionManager
    {
        void PrepareRepository();
        IEnumerable<CodingSession> GenerateRecords(int count);
        void HandleView();
        void HandleInsert();
        void HandleUpdate();
        void HandleDelete();
        

    }
}
