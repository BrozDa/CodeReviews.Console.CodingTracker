using CodingTracker.Interfaces;
using System.Diagnostics;

namespace CodingTracker
{
    internal class SessionTracker : ISessionTracker
    {
        private IInputManager _inputManager;
        private IOutputManager _outputManager;
        private ICodingSessionRepository _codingSessionRepository;

        public SessionTracker(IInputManager inputManager, IOutputManager outputManager, ICodingSessionRepository repository)
        {
            _codingSessionRepository = repository;
            _inputManager = inputManager;
            _outputManager = outputManager;
        }

        public void TrackSession()
        {
            if (!_inputManager.ConfirmTrackingStart())
            { return; }

            Stopwatch stopwatch = new Stopwatch();
            var cursorPosition = Console.GetCursorPosition();

            Console.CursorVisible = false;

            DateTime start = InitializeDate();

            TrackSession(stopwatch, cursorPosition);

            DateTime end = InitializeDate();

            CodingSession trackedSession = new CodingSession { Start = start, End = end };

            _outputManager.PrintTrackedSession(trackedSession);
            _codingSessionRepository.Insert(trackedSession);
        }

        private DateTime InitializeDate()
        {
            DateTime date = DateTime.Now;
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        private void TrackSession(Stopwatch stopwatch, (int left, int top) cursorPosition)
        {
            stopwatch.Start();
            while (!Console.KeyAvailable)
            {
                Console.SetCursorPosition(cursorPosition.left, cursorPosition.top);
                _outputManager.PrintTrackingPanel(stopwatch.Elapsed);
                Thread.Sleep(1000);
            }
            stopwatch.Stop();
            Console.ReadKey(false);
        }
    }
}