using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;

namespace CodingTracker
{
    internal class CodingSessionTrackerApp
    {
        private CodingSessionRepository _repository;
        private DisplayHandler _displayHandler;

        public CodingSessionTrackerApp(DisplayHandler displayHandler, CodingSessionRepository repository)
        {
            _repository = repository;
            _displayHandler = displayHandler;
        }

        public void Run(bool autoFill = false)
        {
            PrepareRepository(autoFill);

            UserChoice choice;

            while (true)
            {
                choice = _displayHandler.GetMenuInput();
                ProcessChoice(choice);
            }
        }
        private void PrepareRepository(bool shouldAutoFill)
        {
            if (!File.Exists(_repository.RepositoryName))
            {
                _repository.CreateRepository();
            }
            if (shouldAutoFill)
            {
                List<CodingSession> sessions = GenerateRecords(100);
                _repository.InsertBulk(sessions);
            }
        }
        private void ProcessChoice(UserChoice choice)
        {
            switch (choice) 
            {
                case UserChoice.Insert:
                    HandleInsert();
                    break;
                case UserChoice.View:
                    HandleView();
                    break;
                case UserChoice.Update:
                    HandleUpdate();
                    break;
                case UserChoice.Delete:
                    HandleDelete();
                    break;
                case UserChoice.Track:
                    HandleTrack();
                    break;
                case UserChoice.Exit:
                    Environment.Exit(0);
                    break;
            }
        }
        private void HandleInsert()
        {
            CodingSession session = GetNewSession();

            if (_displayHandler.ConfirmOperation())
            {
                _repository.Add(session);
            }
        }
        private void HandleView()
        {
            List<CodingSession> sessions = _repository.GetAll().ToList();
            _displayHandler.PrintRecords(sessions);

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        private void HandleUpdate()
        {
            List<CodingSession> sessions = _repository.GetAll().ToList();
            _displayHandler.PrintRecords(sessions);
            CodingSession? originalSession = _displayHandler.GetSessionFromUserInput(sessions, "update");

            if (originalSession == null)
            {
                return;
            }

            CodingSession updatedSession = GetNewSession();
            updatedSession.Id = originalSession.Id;

            _repository.Update(updatedSession);

        }
        private void HandleDelete() 
        {
            List<CodingSession> sessions = _repository.GetAll().ToList();
            _displayHandler.PrintRecords(sessions);
            CodingSession? session = _displayHandler.GetSessionFromUserInput(sessions, "delete");

            if (session == null) 
            {
                return;
            }

            if (_displayHandler.ConfirmOperation())
            {
                _repository.Delete(session);
            }

        }
        
        private void HandleTrack()
        {
            if(!_displayHandler.ConfirmTrackingStart())
                { return; }


            var cursorPosition = Console.GetCursorPosition();
            Console.CursorVisible = false;

            Stopwatch stopwatch = new Stopwatch();
            DateTime start = InitializeDate();

            TrackSession(stopwatch, cursorPosition);

            DateTime end = InitializeDate();

            CodingSession trackedSession = new CodingSession{ Start = start, End = end};

            _displayHandler.PrintTrackedSession(trackedSession);
            _repository.Add(trackedSession);

        }
        private void TrackSession(Stopwatch stopwatch, (int left, int top) cursorPosition)
        {
            stopwatch.Start();
            while (!Console.KeyAvailable)
            {
                Console.SetCursorPosition(cursorPosition.left, cursorPosition.top);
                _displayHandler.PrintTrackingPanel(stopwatch.Elapsed);
                Thread.Sleep(1000);
            }
            stopwatch.Stop();
            Console.ReadKey(false);

        }
        private DateTime InitializeDate()
        {
            DateTime date = DateTime.Now;
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        private CodingSession GetNewSession()
        {
            DateTime start = _displayHandler.GetStartTime();
            DateTime end = _displayHandler.GetEndTime(start);

            CodingSession session = new CodingSession(){ Start = start, End = end };

            return session;
        }
        private List<CodingSession> GenerateRecords(int ammount)
        {
            DateTime start = new DateTime(2020, 01, 01, 00, 00, 00);
            DateTime end;
            Random random = new Random();

            List<CodingSession> records = new List<CodingSession>();

            for (int i = 0; i < ammount; i++)
            {
                end = start.AddHours(random.Next(24));

                records.Add(new CodingSession { Start = start, End = end});

                start = end;
            }

            return records;
        }

    }
}
