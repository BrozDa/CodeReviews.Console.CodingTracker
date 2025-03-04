using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class Report
    {
        private CodingSessionRepository _sessionRepository;
        private ReportDisplayHandler _displayHandler;
        private List<CodingSession> _codingSessions;
        private InputHandler _inputHandler;

        private DateTime start;
        private DateTime end;
        private bool _ascending = true;

        public Report(CodingSessionRepository sessionRepository, ReportDisplayHandler displayHandler, InputHandler inputHandler)
        {
            _sessionRepository = sessionRepository;
            _displayHandler = displayHandler;
            _inputHandler = inputHandler;


        }
        public void Run()
        {
            _codingSessions = _sessionRepository.GetAll().ToList();
            ReportChoice choice = _displayHandler.GetTimeFrame();

            while (choice != ReportChoice.Exit)
            {
                SetTimeRange(choice);
                _ascending = _displayHandler.ShouldBeOutputAsceding();
               
                List<CodingSession> filteredResultsSQL = _sessionRepository.GetDataWithinRange(start, end).ToList();

                filteredResultsSQL.OrderBy(c => c.Start);

                if (!_ascending) 
                { 
                    filteredResultsSQL.Reverse();
                }

                choice = _displayHandler.GetTimeFrame();
            }
        }
        public void SetTimeRange(ReportChoice choice)
        {
            switch (choice) {
                case ReportChoice.ThisYear:
                    start = new DateTime(DateTime.Now.Year,1,1,0,0,0);
                    end = new DateTime(DateTime.Now.AddYears(1).Year, 1, 1, 0, 0, 0);
                    break;
                case ReportChoice.ThisMonth:
                    start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                    start = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 1, 0, 0, 0);
                    break;
                case ReportChoice.ThisWeek:
                    start = GetStartOfThisWeek();
                    end = start.AddDays(7);
                    break;
                case ReportChoice.Custom:
                    start = _inputHandler.GetStartTime();
                    end = _inputHandler.GetEndTime(start).AddDays(1);
                    break;
                case ReportChoice.Exit:
                    return;
            }
        }

        private DateTime GetStartOfThisWeek()
        {
            var difference = DateTime.Now.DayOfWeek - DayOfWeek.Monday;

            return DateTime.Now.AddDays(difference);
        }
        

    }
}
