using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace CodingTracker
{
    internal class CodingSessionTrackerApp
    {
        private CodingSessionRepository _repository;
        private AppDisplayHandler _displayHandler;

        private bool autoFill = true;

        public CodingSessionTrackerApp(AppDisplayHandler displayHandler, CodingSessionRepository repository)
        {
            _repository = repository;
            _displayHandler = displayHandler;
        }

        public void Run()
        {
            PrepareRepository(_repository.RepositoryPath);

            UserChoice choice;

            choice = _displayHandler.GetMenuInput();

            while (choice != UserChoice.Exit)
            {
                ProcessChoice(choice);
                choice = _displayHandler.GetMenuInput();
                
            }
        }
        public void PrepareRepository(string path)
        {
            if (!DoesRepositoryExists(path))
            {
                _repository.CreateRepository();
                if (autoFill)
                {
                    List<CodingSession> sessions = GenerateRecords(100);
                    _repository.InsertBulk(sessions);
                }
            }
        }
        private bool DoesRepositoryExists(string path) => File.Exists(_repository.RepositoryPath);
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
                case UserChoice.Report:
                    HandleReport();
                    break;
                case UserChoice.Exit:
                    return;
            }
        }
        private void HandleInsert()
        {
            CodingSession session = GetNewSession();

            if (_displayHandler.ConfirmOperation(session,"insert"))
            {
                _repository.Insert(session);
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
            
            if (_displayHandler.ConfirmOperation(session, "delete"))
            {
                _repository.Delete(session);
            }

        }
        private void HandleReport()
        {
            //Report report = new Report(_repository, reportDisplayHandler, InputHandler);


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
            _repository.Insert(trackedSession);

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
            DateTime start = new DateTime(2024, 01, 01, 00, 00, 00);
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
