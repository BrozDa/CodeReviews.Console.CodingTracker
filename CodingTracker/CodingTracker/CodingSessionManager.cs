using CodingTracker.Interfaces;

namespace CodingTracker
{
    internal class CodingSessionManager : ICodingSessionManager
    {
        private IIntputManager _inputManager;
        private IOutpuManager _outputManager;
        private ICodingSessionRepository _sessionRepository;

        public CodingSessionManager(IIntputManager inputManager, IOutpuManager outputManager, ICodingSessionRepository repository)
        {
            _sessionRepository = repository;
            _inputManager = inputManager;
            _outputManager = outputManager;
        }

        public void PrepareRepository()
        {
            if (!File.Exists(_sessionRepository.RepositoryPath))
            {
                _sessionRepository.CreateRepository();

                IEnumerable<CodingSession> sessions = GenerateRecords(100);
                _sessionRepository.InsertBulk(sessions);
            }
        }
        public IEnumerable<CodingSession> GenerateRecords(int count)
        {
            DateTime start = new DateTime(2025, 02, 01, 00, 00, 00);
            DateTime end;
            Random random = new Random();

            List<CodingSession> records = new List<CodingSession>();

            for (int i = 0; i < count; i++)
            {
                end = start.AddHours(random.Next(24));

                records.Add(new CodingSession { Start = start, End = end });

                start = end;
            }

            return records;
        }
        public void HandleView()
        {
            List<CodingSession> sessions = _sessionRepository.GetAll().ToList();
            _outputManager.PrintRecords(sessions);

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public void HandleInsert()
        {
            CodingSession session = _inputManager.GetNewSession();

            if (_inputManager.ConfirmOperation(session, "add"))
            {
                _sessionRepository.Insert(session);
            }
        }
        public void HandleUpdate()
        {
            List<CodingSession> sessions = _sessionRepository.GetAll().ToList();
            _outputManager.PrintRecords(sessions);

            CodingSession? originalSession = _inputManager.GetSessionFromUserInput(sessions, "update");

            if (originalSession == null)
            {
                return;
            }

            CodingSession updatedSession = _inputManager.GetNewSession();
            updatedSession.Id = originalSession.Id;

            if (_inputManager.ConfirmOperation(originalSession, "update"))
            {
                _sessionRepository.Update(updatedSession);
            }


        }
        public void HandleDelete()
        {
            List<CodingSession> sessions = _sessionRepository.GetAll().ToList();
            _outputManager.PrintRecords(sessions);

            CodingSession? session = _inputManager.GetSessionFromUserInput(sessions, "delete");


            if (session == null)
            {
                return;
            }

            if (_inputManager.ConfirmOperation(session, "delete"))
            {
                _sessionRepository.Delete(session);
            }

        }

        
    }
}
