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

        private void ConsoleClear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
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
                        session.Start.ToString(_dateTimeFormat),
                        session.End.ToString(_dateTimeFormat),
                        session.Duration.ToString()
                    });
            }
            AnsiConsole.Write(table);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

    }
}
