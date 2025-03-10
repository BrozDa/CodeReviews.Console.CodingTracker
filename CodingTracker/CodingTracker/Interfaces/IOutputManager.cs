namespace CodingTracker
{
    internal interface IOutputManager
    {
        void ConsoleClear();

        void PrintMenuHeader();

        void PrintRecords(List<CodingSession> sessions);

        void PrintTrackingPanel(TimeSpan elapsed);

        void PrintTrackedSession(CodingSession session);
    }
}