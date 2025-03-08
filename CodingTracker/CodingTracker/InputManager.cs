using CodingTracker.Interfaces;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker
{
    internal class InputManager: IIntputManager
    {
        private readonly string _dateTimeFormat;

        public InputManager(string dateTimeFormat)
        {
            _dateTimeFormat = dateTimeFormat;
        }
        public UserChoice GetMenuInput()
        {
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
                    UserChoice.Report => "Generate Report",
                    UserChoice.Exit => "Exit the application",
                }));
            return input;
        }
        public CodingSession GetNewSession()
        {
            DateTime start = GetStartTime();
            DateTime end = GetEndTime(start);

            return new CodingSession() { Start = start, End = end };
        }
        public DateTime GetStartTime()
        {
            DateTime startDate = DateTime.Now;

            AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter start date and time in format [green]{_dateTimeFormat.ToUpper()}[/]: ")
                .Validate(input =>
                            ParseDateTimeInput(input, out startDate))
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

                    if (startDate >= endDate)
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
        public CodingSession? GetSessionFromUserInput(List<CodingSession> sessions, string operation)
        {
            HashSet<int> ids = new HashSet<int>(sessions.Select(x => x.Id));

            int id = AnsiConsole.Prompt(
                new TextPrompt<int>($"Enter ID of record you with to [green]{operation}[/] or 0 to go back to main menu: ")
                .Validate(x => ids.Contains(x) || x == 0)
                );

            return id == 0 ? null : sessions.Find(x => x.Id == id);
        }

        public bool ConfirmOperation(CodingSession session, string operation)
        {
            Console.WriteLine();

            string prompt = $"Coding session which started {session.Start.ToString(_dateTimeFormat)}, " +
                $"ended {session.End.ToString(_dateTimeFormat)} " +
                $"and lasted for {(int)session.Duration.TotalMinutes} minutes will be {operation}d\n" +
                $"Please confirm";

            return AnsiConsole.Confirm(prompt);
        }
        public bool ConfirmTrackingStart()
        {
            ConsoleKey input;
            do
            {
                Console.WriteLine("Press 'ENTER' to start tracking or 'ESC' to exit: ");
                input = Console.ReadKey(true).Key;

            } while ((input != ConsoleKey.Enter) && (input != ConsoleKey.Escape));

            return input == ConsoleKey.Enter;
        }
        public ReportTimeFrame GetTimeRangeForReport()
        {

            ReportTimeFrame[] reportTimeFrames = (ReportTimeFrame[])Enum.GetValues(typeof(ReportTimeFrame));

            ReportTimeFrame input = AnsiConsole.Prompt(
                new SelectionPrompt<ReportTimeFrame>()
                .AddChoices(reportTimeFrames)
                .WrapAround(true)
                .UseConverter((x) => x switch
                {
                    ReportTimeFrame.ThisYear => "This Year",
                    ReportTimeFrame.ThisMonth => "This Month",
                    ReportTimeFrame.ThisWeek => "This Week",
                    ReportTimeFrame.Custom => "Custom",
                }));

            return input;
        }

        
    }
}
