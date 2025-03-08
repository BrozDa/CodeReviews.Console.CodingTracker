using CodingTracker.Interfaces;
using System.Diagnostics;
using System.Globalization;

namespace CodingTracker
{
    internal class CodingSessionTrackerApp
    {
        private ICodingSessionManager _codingSessionManager;
        private IIntputManager _inputManager;
        private IOutpuManager _outputManager;
        private ISessionTracker _sessionTracker;
        private IReportManager _reportManager;

        public CodingSessionTrackerApp(IIntputManager inputManager, IOutpuManager outputManager, ICodingSessionManager codingSessionManager, ISessionTracker sessionTracker, IReportManager reportManager)
        {
            _inputManager = inputManager;
            _outputManager = outputManager;
            _sessionTracker = sessionTracker;
            _codingSessionManager = codingSessionManager;
            _reportManager = reportManager;
        }

        public void Run()
        {
            _codingSessionManager.PrepareRepository();

            UserChoice choice;

            _outputManager.PrintMenuHeader();
            choice = _inputManager.GetMenuInput();

            while (choice != UserChoice.Exit)
            {
                ProcessChoice(choice);

                _outputManager.ConsoleClear();
                _outputManager.PrintMenuHeader();

                choice = _inputManager.GetMenuInput();
                
            }
        }
   
        private void ProcessChoice(UserChoice choice)
        {
            switch (choice) 
            {
                case UserChoice.Insert:
                    _codingSessionManager.HandleInsert();
                    break;
                case UserChoice.View:
                    _codingSessionManager.HandleView();
                    break;
                case UserChoice.Update:
                    _codingSessionManager.HandleUpdate();
                    break;
                case UserChoice.Delete:
                    _codingSessionManager.HandleDelete();
                    break;
                case UserChoice.Track:
                    _sessionTracker.TrackSession();
                    break;
                case UserChoice.Report:
                    _reportManager.GetReport();
                    break;
                case UserChoice.Exit:
                    return;
            }
        }
        

    }
}
