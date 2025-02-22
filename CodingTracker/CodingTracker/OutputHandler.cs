using Spectre.Console;
using System.Runtime.Serialization;


namespace CodingTracker
{
    internal class OutputHandler
    {
        private readonly string _dateTimeFormat;
        public OutputHandler(string dateTimeFormat)
        {
            _dateTimeFormat = dateTimeFormat;
        }

        public void PrintMenuHeader()
        {
            Console.WriteLine("Welcome to Coding Tracker application");
            Console.WriteLine("This application allow you to manually add sessions from past or track a session as it happen");
            Console.WriteLine();
        }
        public void PrintRecords(List<CodingSession> sessions)
        {
            if (sessions == null || sessions.Count == 0)
            {
                Console.WriteLine("No records in database"); 
                return;
            }

            var table = new Table();
            table.Title = new TableTitle("Coding Sessions");
            table.AddColumns(
                    new TableColumn("ID").Centered(),
                    new TableColumn("Start").Centered(),
                    new TableColumn("End").Centered(),
                    new TableColumn("Duration").Centered()
            );
            foreach (var session in sessions)
            {
                table.AddRow(new string[]
                    {
                        session.Id.ToString(),
                        session.StartDateTime.ToString(_dateTimeFormat),
                        session.EndDateTime.ToString(_dateTimeFormat),
                        string.Format("{0:hh} h {0:mm} m", session.DurationTimeSpan),
                    });
            }
            AnsiConsole.Write(table);
        }

    }
}
