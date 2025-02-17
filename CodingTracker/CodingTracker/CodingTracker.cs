

using System.IO.Pipes;

namespace CodingTracker
{
    /// <summary>
    /// Represents user application for tracking coding time
    /// </summary>
    
    internal class CodingTracker
    {
        private DatabaseManager _databaseManager;
        private InputManager _inputManager;
        private OutputManager _outputManager;
        private readonly string _tableName = "Sessions";
        //private SessionTracker _sessionTracker;

        public CodingTracker(DatabaseManager databaseManager, InputManager inputManager, OutputManager outputManager)
        {
            _databaseManager = databaseManager;
            _inputManager = inputManager;
            _outputManager = outputManager;
        }
        public void Start()
        {
            if (!_databaseManager.DoesDatabaseExists())
            {
                _databaseManager.CreateDatabase();
                _databaseManager.CreateTable();
            }

            while (true)
            {
                ProcessMainMenu();
            }
        }
        public void ProcessMainMenu()
        {
            UserChoice menuChoice = _outputManager.PrintMenu();

            switch (menuChoice)
            {
                case UserChoice.ViewRecords:
                    break;
                case UserChoice.AddSession:
                    HandleAddSession();
                    break;
                case UserChoice.RemoveSession:
                    break;
                case UserChoice.UpdateSession:
                    break;
                case UserChoice.TrackSession:
                    break;
                case UserChoice.ExitApplication:
                    Environment.Exit(0);
                    break;

            }
        }
        public void HandleViewRecords()
        {
            List<CodingRecord> sessions = _databaseManager.GetRecords();
            _outputManager.PrintTable(_tableName, sessions);
        }
        public void HandleAddSession()
        {
            CodingRecord newRecord = _inputManager.GetNewRecord();
            _databaseManager.InsertRecord(newRecord);
        }
        public void HandleRemoveSession()
        {
            throw new NotImplementedException();
        }
        public void HandleUpdateSession()
        {
            throw new NotImplementedException();
        }
        public void HandleTrackSession()
        {
            throw new NotImplementedException();
        }
    }
}
