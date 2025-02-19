using Spectre.Console;
using System.Globalization;

namespace CodingTracker
{
    /// <summary>
    /// Manages all input handling and verification of said input
    /// </summary>
    internal class InputManager
    {
        private string DateTimeFormat { get; init; }
        public InputManager(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
        public CodingRecord GetNewRecord()
        {
            Console.WriteLine($"Please enter date and time in {DateTimeFormat.ToUpper()} format");
            Console.WriteLine();

            DateTime start = GetStartTime();
            DateTime end = GetEndTime(start);
            return new CodingRecord(start, end, (end-start).Duration());
        }
        public DateTime GetStartTime()
        {
            DateTime startdate = DateTime.Now;

            var start = AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter start date and time: ")
                .DefaultValue(DateTime.Now.ToString(DateTimeFormat))
                .Validate((input) =>
                     DateTime.TryParseExact(input, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startdate)
                )
                .ValidationErrorMessage("Invalid input format"));

            return startdate;
        }
        public DateTime GetEndTime(DateTime start)
        {
            DateTime endDate = DateTime.Now;

            var endd = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter end date and time: ")
                .DefaultValue(DateTime.Now.ToString(DateTimeFormat))
                .Validate((input) =>
                    {
                        if (!DateTime.TryParseExact(input, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                            return ValidationResult.Error("Invalid format passed");
                        if (endDate < start)
                            return ValidationResult.Error("End cannot be equal or smaller to start");

                        return ValidationResult.Success();
                    }
                ));

            return endDate;
        }
    }
}
