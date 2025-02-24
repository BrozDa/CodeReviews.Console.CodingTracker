using Spectre.Console;
using System.Globalization;

namespace CodingTracker
{
    internal class DisplayHandler
    {
        private readonly string _dateTimeFormat;

        public InputHandler Input { get; init; }
        public OutputHandler Output { get; init; }
        public DisplayHandler(InputHandler inputHandler, OutputHandler outputHandler, string dateTimeFormat)
        {
            Input = inputHandler;
            Output = outputHandler;
            _dateTimeFormat = dateTimeFormat;
        }
        private void ConsoleClear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
        public UserChoice GetMenuInput()
        {
            ConsoleClear();

            PrintMenuHeader();

            UserChoice[] menuOptions = (UserChoice[])Enum.GetValues(typeof(UserChoice));

            UserChoice input = AnsiConsole.Prompt(
                new SelectionPrompt<UserChoice>()
                .AddChoices(menuOptions)
                .WrapAround(true)
                .UseConverter((x) => x switch
                {
                    UserChoice.View => "View sessions",
                    UserChoice.Insert => "Add session",
                    UserChoice.Delete => "Remove session",
                    UserChoice.Update => "Update session",
                    UserChoice.Track => "Start to track session",
                    UserChoice.Exit => "Exit the application",
                }));
            return input;
        }

        public DateTime GetStartTime()
        {
            DateTime startDate = DateTime.Now;

            AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter start date and time in format [green]{_dateTimeFormat.ToUpper()}[/]: ")
                .Validate(input => ParseDateTimeInput(input, out startDate))
                .ValidationErrorMessage("[red]Invalid input format[/]")
                );

            return startDate;
        }


        public DateTime GetEndTime(DateTime startDate)
        {
            DateTime endDate = DateTime.Now;
            AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter end date and time in format [green]{_dateTimeFormat.ToUpper()}[/]: ")
                .Validate((input) =>
                {
                    if (!ParseDateTimeInput(input, out endDate))
                    {
                        return ValidationResult.Error("[red]Invalid input format[/]");
                    }

                    if (!ValidateEndDate(startDate, endDate))
                    {
                        return ValidationResult.Error("[red]End is sooner than start[/]");
                    }

                    return ValidationResult.Success();

                }));

            return endDate;
        }

        private bool ParseDateTimeInput(string start, out DateTime parsed)
        {
            return DateTime.TryParseExact(start, _dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed);

        }
        private bool ValidateEndDate(DateTime start, DateTime end)
        {
            return end >= start;
        }

        public int GetSessionId(List<CodingSession> sessions, string operation)
        {
            HashSet<int> ids = new HashSet<int>(sessions.Select(x => x.Id));

            int id = AnsiConsole.Prompt(
                new TextPrompt<int>($"Enter ID of record you with to [green]{operation}[/]: ")
                .Validate(x => ids.Contains(x))
                .ValidationErrorMessage("[red]Entered ID is not present in the Database[/]")
                );

            return id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessions"></param>
        /// <param name="operation"></param>
        /// <returns>Null refference in case user decided to exit, Valid Coding session otherwise</returns>
        public CodingSession? GetSessionFromUserInput(List<CodingSession> sessions, string operation)
        {
            HashSet<int> ids = new HashSet<int>(sessions.Select(x => x.Id));

            int id = AnsiConsole.Prompt(
                new TextPrompt<int>($"Enter ID of record you with to [green]{operation}[/] or 0 to go back to main menu: ")
                .Validate(x => ids.Contains(x) || x == 0)
                );

            return id == 0 ? null : sessions.Find(x => x.Id == id);
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
                        session.Duration.ToString(),
                    });
            }
            AnsiConsole.Write(table);
        }
        public bool ConfirmOperation()
        {
            Console.WriteLine();

            var input = AnsiConsole.Prompt(
                new TextPrompt<bool>("\n" + "Please confirm your input: ")
                .DefaultValue(true)
                .AddChoice(true)
                .AddChoice(false)
                .WithConverter(choice => choice ? "y" : "n")
                );

            return input;
        }
        public bool ConfirmTrackingStart()
        {
            Console.WriteLine("Press 'ENTER' to start tracking or 'ESC' to exit: ");
            ConsoleKey input = Console.ReadKey().Key;

            while((input != ConsoleKey.Enter) && (input != ConsoleKey.Escape)) 
            {
                Console.WriteLine("Press 'ENTER' to start tracking or 'ESC' to exit");
                input = Console.ReadKey().Key;
            }

            return input == ConsoleKey.Enter;
        }
        public void PrintTrackingPanel(TimeSpan elapsed)
        {
            var panel = new Panel($"Session is being tracked\n\n" +
                $"Current time: {elapsed.ToString(@"hh\:mm\:ss")}\n\n" +
                $"[red]Press any key to stop tracking[/]");
            panel.Padding = new Padding(2,1,2,1);
            AnsiConsole.Write(panel);
        }
        public void PrintTrackedSession(CodingSession session)
        {
            Console.WriteLine();
            Console.WriteLine($"Session started {session.Start.ToString(_dateTimeFormat)}\n" +
                $"Session ended: {session.End.ToString(_dateTimeFormat)}\n" +
                $"Session duration: {session.Duration.ToString(@"hh\:mm\:ss")}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
