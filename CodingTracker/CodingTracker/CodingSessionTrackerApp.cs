using System.Security.Cryptography;

namespace CodingTracker
{
    internal class CodingSessionTrackerApp
    {
        private InputHandler _inputHandler;
        private OutputHandler _outputHandler;
        private CodingSessionRepository _repository;

        public CodingSessionTrackerApp(InputHandler inputHandler, OutputHandler outputHandler, CodingSessionRepository repository)
        {
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
            _repository = repository;
        }

        public void Run()
        {
            //_repository.CreateRepository();
            _outputHandler.PrintMenuHeader();
            UserChoice choice;
            while (true)
            {
                choice = _inputHandler.PrintMenuAndGetInput();
                ProcessChoice(choice);
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
                case UserChoice.Exit:
                    throw new NotImplementedException();
                    break;
            }
        }
        private void HandleInsert()
        {
            CodingSession session = GetNewSession();
  
            _repository.Add(session);
        }
        private void HandleView()
        {
            List<CodingSession> sessions = _repository.GetAll().ToList();
            _outputHandler.PrintRecords(sessions);
        }
        private void HandleDelete() 
        {
            List<CodingSession> sessions = _repository.GetAll().ToList();
            _outputHandler.PrintRecords(sessions);
            int id = _inputHandler.GetSessionId(sessions, "delete");
            _repository.Delete(id);

        }
        private void HandleUpdate()
        {
            List<CodingSession> sessions = _repository.GetAll().ToList();
            _outputHandler.PrintRecords(sessions);
            int id = _inputHandler.GetSessionId(sessions, "update");
            CodingSession session = GetNewSession();
            session.Id = id;

            _repository.Update(session);

        }
        private CodingSession GetNewSession()
        {
            DateTime start = _inputHandler.GetStartTime();
            DateTime end = _inputHandler.GetEndTime(start);
            TimeSpan duration = (end - start).Duration();

            CodingSession session = new CodingSession()
            {
                Start = start.ToString(),
                End = end.ToString(),
                Duration = duration.ToString()
            };

            return session;
        }

    }
}
