namespace CodingTracker
{
    internal interface IOutpuManager
    {
        void ConsoleClear();
        void PrintMenuHeader();
        void PrintRecords(List<CodingSession> sessions);

        void PrintTrackingPanel(TimeSpan elapsed);
        void PrintTrackedSession(CodingSession session);

    }
}
