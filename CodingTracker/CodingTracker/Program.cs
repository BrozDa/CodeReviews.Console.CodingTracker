using System.Configuration;

namespace CodingTracker
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            string defaultConnectionString = "Data Source=coding-tracker.sqlite;Version=3;";
            string defaultRepositoryName = "coding-tracker.sqlite";

            string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            connectionString ??= defaultConnectionString;

            string? repositoryName = ConfigurationManager.AppSettings.Get("DatabaseName");
            repositoryName ??= defaultRepositoryName;

            string dateTimeFormat = "dd-MM-yyyy HH:mm";

            InputHandler inputHandler = new InputHandler(dateTimeFormat);
            OutputHandler outputHandler = new OutputHandler(dateTimeFormat);
            CodingSessionRepository repository = new CodingSessionRepository(connectionString, repositoryName);
            DisplayHandler displayHandler= new DisplayHandler(inputHandler, outputHandler,dateTimeFormat);
            CodingSessionTrackerApp app = new CodingSessionTrackerApp(displayHandler, repository);

            app.Run();


        }
    }
}
