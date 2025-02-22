using Spectre.Console;
using System.Globalization;
using System.Runtime.Serialization;

namespace CodingTracker
{
    internal class InputHandler
    {
        private readonly string _dateTimeFormat;
        public InputHandler(string dateTimeFormat)
        {
            _dateTimeFormat = dateTimeFormat;
        }

        public UserChoice PrintMenuAndGetInput()
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
                    UserChoice.Exit => "Exit the application",
                }));

            return input;
        }

        public DateTime GetStartTime()
        {
            DateTime startdate = DateTime.Now;

            AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter start date and time in format {_dateTimeFormat.ToUpper()}: ")
                .DefaultValue(startdate.ToString(_dateTimeFormat))
                .Validate(input => ParseDateTimeInput(input, out startdate), "Invalid input format")
                );

            return startdate;
        }


        public DateTime GetEndTime(DateTime start)
        {
            DateTime endDate = DateTime.Now;

            AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter end date and time in format {_dateTimeFormat.ToUpper()}: ")
                .DefaultValue(start.AddMinutes(1).ToString(_dateTimeFormat))
                .Validate(input =>
                                ParseDateTimeInput(input, out endDate) 
                                && ValidateEndDate(endDate, start), "Invalid input format or end is sooner than start")      
                );
            return endDate;
        }

        private bool ParseDateTimeInput(string start, out DateTime parsed)
        {
            return DateTime.TryParseExact(start, _dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed);
                        
        }
        private bool ValidateEndDate(DateTime end, DateTime start)
        {
            return end.Subtract(start.AddMinutes(1)) > TimeSpan.FromMinutes(1);
        }
        public int GetSessionId(List<CodingSession> sessions, string operation)
        {
            HashSet<int> ids = new HashSet<int>(sessions.Select(x=> x.Id));

            int id = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter ID of record you with to " + operation)
                .Validate(x => ids.Contains(x))
                );
            
            return id;
        }
        public CodingSession GetSession(List<CodingSession> sessions, string operation)
        {
            HashSet<int> ids = new HashSet<int>(sessions.Select(x => x.Id));

            int id = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter ID of record you with to " + operation)
                .Validate(x => ids.Contains(x))
                );

            return sessions.Find(x => x.Id == id)!;
        }

    }
}
