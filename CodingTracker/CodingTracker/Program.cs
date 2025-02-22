using System.Configuration;
using System.Collections.Specialized;

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
            CodingSessionRepository repository = new CodingSessionRepository(connectionString);
            CodingSessionTrackerApp app = new CodingSessionTrackerApp(inputHandler, outputHandler, repository);

            app.Run();
           /* DateTime now = DateTime.Now;
            DateTime plusMin = now.AddMinutes(1);

            Console.WriteLine($"Now: {now.ToString()}, plus Min: {plusMin.ToString()}");

            TimeSpan diff = plusMin.Subtract( now );

            Console.WriteLine($"Diff: {diff}");

            Console.WriteLine(diff.TotalMinutes <=1);*/

            /*InputManager inputManager = new InputManager(dateTimeFormat);
            OutputManager outputManager = new OutputManager(dateTimeFormat);
            DatabaseManager databaseManager = new DatabaseManager(connectionString, databaseName);

            CodingTracker tracker = new CodingTracker(databaseManager, inputManager, outputManager);
            tracker.Start();


            string s = "a";
            s = s.ToLower().Trim().ToLower().ToUpper().Replace("a", "b");
            Console.WriteLine(s);*/

            /*CodingSessionRepository repository = new CodingSessionRepository(connectionString);
            CodingSession session = new CodingSession() { Start = "now", End = "Later", Duration = "LongAF" };

            //repository.CreateRepository();
            for (int i = 0; i < 10; i++)
            {
                repository.Add(session);
            }
          
            List<CodingSession> sessions = repository.GetAll().ToList();
            foreach (var s in sessions)
            {
                Console.WriteLine(s.ToString());
            }

            Console.WriteLine("------------------------------------");
            int len = sessions.Count;

            CodingSession sessionUpdate = new CodingSession() 
            {
                Id = sessions[len-1].Id,
                Start = sessions[len-1].Start,
                End = sessions[len-1].End,
                Duration = "EvenLonger"
            };

            repository.Update(sessionUpdate);

            sessions = repository.GetAll().ToList();
            foreach (var s in sessions)
            {
                Console.WriteLine(s.ToString());
            }*/

        }
    }
}
