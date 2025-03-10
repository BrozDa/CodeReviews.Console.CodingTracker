using CodingTracker.Interfaces;

namespace CodingTracker
{
    internal class ReportManager : IReportManager
    {
        private IInputManager _inputManager;
        private IOutputManager _outputManager;
        private ICodingSessionRepository _codingSessionRepository;

        public ReportManager(IInputManager inputManager, IOutputManager outputManager, ICodingSessionRepository repository)
        {
            _codingSessionRepository = repository;
            _inputManager = inputManager;
            _outputManager = outputManager;
        }

        public void GetReport()
        {
            ReportTimeFrame timeFrame = _inputManager.GetTimeRangeForReport();
            DateTime start = GetStartDateForReport(timeFrame);
            DateTime end = GetEndDateForReport(timeFrame, start);

            var sessions = _codingSessionRepository.GetDataWithinRange(start, end).ToList();
            _outputManager.PrintRecords(sessions);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private DateTime GetStartDateForReport(ReportTimeFrame timeFrame)
        {
            return timeFrame switch
            {
                ReportTimeFrame.ThisYear => new DateTime(DateTime.Now.Year, 1, 1),
                ReportTimeFrame.ThisMonth => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                ReportTimeFrame.ThisWeek => GetFirstDayOfWeek(),
                ReportTimeFrame.Custom => _inputManager.GetStartTime()
            };
        }

        private DateTime GetFirstDayOfWeek()
        {
            DateTime today = DateTime.Now;
            int offset = today.DayOfWeek - DayOfWeek.Monday;
            today = today.AddDays(offset);
            return today;
        }

        private DateTime GetEndDateForReport(ReportTimeFrame timeFrame, DateTime startDate)
        {
            DateTime today = DateTime.Now;

            return timeFrame switch
            {
                ReportTimeFrame.ThisYear => today,
                ReportTimeFrame.ThisMonth => today,
                ReportTimeFrame.ThisWeek => today,
                ReportTimeFrame.Custom => _inputManager.GetEndTime(startDate)
            };
        }
    }
}