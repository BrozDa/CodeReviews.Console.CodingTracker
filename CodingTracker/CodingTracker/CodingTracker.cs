

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
        //private SessionTracker _sessionTracker;

        public CodingTracker(DatabaseManager databaseManager, InputManager inputManager, OutputManager outputManager)
        {
            _databaseManager = databaseManager;
            _inputManager = inputManager;
            _outputManager = outputManager;
        }
        public void Start()
        {
            while (true)
            {

            }
        }
    }
}
