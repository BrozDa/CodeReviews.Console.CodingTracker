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


            CodingSessionRepository repository = new CodingSessionRepository(connectionString, repositoryName);
            AppDisplayHandler AppDisplayHandler= new AppDisplayHandler(dateTimeFormat);

            CodingSessionTrackerApp app = new CodingSessionTrackerApp(AppDisplayHandler, repository);

            InputHandler InputHandler = new InputHandler(dateTimeFormat);

            ReportDisplayHandler reportDisplayHandler = new ReportDisplayHandler();
            Report report = new Report(repository, reportDisplayHandler, InputHandler);
            
            //report.Run();
            app.Run();


        }
    }
}
