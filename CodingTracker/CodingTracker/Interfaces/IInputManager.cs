namespace CodingTracker.Interfaces
{
    internal interface IInputManager
    {
        UserChoice GetMenuInput();

        CodingSession GetNewSession();

        DateTime GetStartTime();

        DateTime GetEndTime(DateTime startDate);

        bool ConfirmOperation(CodingSession session, string operation);
        bool ConfirmUpdate(CodingSession original, CodingSession updated, string operation);
        CodingSession? GetSessionFromUserInput(List<CodingSession> sessions, string operation);

        bool ConfirmTrackingStart();

        public ReportTimeFrame GetTimeRangeForReport();
    }
}