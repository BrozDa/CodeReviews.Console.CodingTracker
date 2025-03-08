using CodingTracker.Interfaces;
using System.Configuration;

namespace CodingTracker
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            string defaultConnectionString = "Data Source=coding-tracker.sqlite;Version=3;";
            string defaultRepositoryPath = "coding-tracker.sqlite";
            

            string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            connectionString ??= defaultConnectionString;

            string? repositoryName = ConfigurationManager.AppSettings.Get("DatabaseName");
            repositoryName ??= defaultRepositoryPath;

            string dateTimeFormat = "dd-MM-yyyy HH:mm";


            ICodingSessionRepository repository = new CodingSessionRepository(connectionString, repositoryName);

            IIntputManager inputManager = new InputManager(dateTimeFormat);
            IOutpuManager outputManager = new OutputManager(dateTimeFormat);

            IReportManager reportManager = new ReportManager(inputManager, outputManager, repository);

            ISessionTracker sessionTracker = new SessionTracker(inputManager, outputManager, repository);

            ICodingSessionManager sessionManager = new CodingSessionManager(inputManager, outputManager, repository);

            

            CodingSessionTrackerApp app = new CodingSessionTrackerApp(inputManager, outputManager, sessionManager, sessionTracker, reportManager);

 
            
            //report.Run();
            app.Run();


        }
    }
}
