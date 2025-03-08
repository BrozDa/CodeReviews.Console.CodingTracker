using Spectre.Console;

namespace CodingTracker
{
    internal class OutputManager: IOutpuManager
    {
        private readonly string _dateTimeFormat;

        public OutputManager(string dateTimeFormat)
        {
            _dateTimeFormat = dateTimeFormat;
        }
        public void ConsoleClear()
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
            ConsoleClear();
            if (sessions == null || sessions.Count == 0)
            {
                Console.WriteLine("No records in database");
                return;
            }

            var table = new Table();
            table.Title = new TableTitle("Coding Sessions");
            table.AddColumns("ID", "Start", "End", "Duration").Centered();

            foreach (var session in sessions)
            {
                table.AddRow(
                        session.Id.ToString(),
                        session.Start.ToString(_dateTimeFormat),
                        session.End.ToString(_dateTimeFormat),
                        $"{(int)session.Duration.TotalMinutes} min"
                    );
            }
            AnsiConsole.Write(table);
        }
        public void PrintTrackingPanel(TimeSpan elapsed)
        {
            var panel = new Panel($"Session is being tracked\n\n" +
                                  $"Current time: {elapsed.ToString(@"hh\:mm\:ss")}\n\n" +
                                  $"[red]Press any key to stop tracking[/]")
                            .Padding(2, 1, 2, 1);

            AnsiConsole.Write(panel);
        }
        public void PrintTrackedSession(CodingSession session)
        {
            Console.WriteLine();
            Console.WriteLine($"Session started {session.Start.ToString(_dateTimeFormat)}\n" +
                $"Session ended: {session.End.ToString(_dateTimeFormat)}\n" +
                $"Session duration: {session.Duration.ToString(@"hh\:mm\:ss")}" +
                $"\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
