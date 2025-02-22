using System.Configuration;
using System.Collections.Specialized;

namespace CodingTracker
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            string databaseName = ConfigurationManager.AppSettings.Get("DatabaseName");
            string dateTimeFormat = "dd-MM-yyyy HH:mm";

            /*InputManager inputManager = new InputManager(dateTimeFormat);
            OutputManager outputManager = new OutputManager(dateTimeFormat);
            DatabaseManager databaseManager = new DatabaseManager(connectionString, databaseName);

            CodingTracker tracker = new CodingTracker(databaseManager, inputManager, outputManager);
            tracker.Start();


            string s = "a";
            s = s.ToLower().Trim().ToLower().ToUpper().Replace("a", "b");
            Console.WriteLine(s);*/

            CodingSessionRepository repository = new CodingSessionRepository(connectionString);
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
            }




        }
    }
}
