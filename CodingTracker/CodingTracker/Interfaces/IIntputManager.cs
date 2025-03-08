namespace CodingTracker.Interfaces
{
    internal interface IIntputManager
    {
        UserChoice GetMenuInput();
        CodingSession GetNewSession();
        DateTime GetStartTime();
        DateTime GetEndTime(DateTime startDate);
        bool ConfirmOperation(CodingSession session, string operation);
        CodingSession? GetSessionFromUserInput(List<CodingSession> sessions, string operation);
        bool ConfirmTrackingStart();
        public ReportTimeFrame GetTimeRangeForReport();
    }
}
