using Spectre.Console;
using System;


namespace CodingTracker
{
    /// <summary>
    /// Manages all output to user's screen
    /// </summary>
    internal class OutputManager
    {
        private string DateTimeFormat { get; init; }
        public OutputManager(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
        public void PrintMenuHeader()
        {
            Console.WriteLine("Welcome to Coding Tracker application");
            Console.WriteLine("This application allow you to manually add sessions from past or track a session as it happen");
            Console.WriteLine();
        }

        public UserChoice PrintMenu()
        {
            UserChoice[] menuOptions = (UserChoice[])Enum.GetValues(typeof(UserChoice));

            UserChoice input = AnsiConsole.Prompt(
                new SelectionPrompt<UserChoice>()
                .Title("Enter:")
                .AddChoices(menuOptions)
                .WrapAround(true)
                .UseConverter((x) => x switch
                {
                    UserChoice.ViewRecords => "View sessions",
                    UserChoice.AddSession => "Add session",
                    UserChoice.RemoveSession => "Remove session",
                    UserChoice.UpdateSession => "Update session",
                    UserChoice.TrackSession => "Start to track session",
                    UserChoice.ExitApplication => "Exit the application",
                    _ => throw new Exception("Invalid value passed")
                }));

            return input;
        }
        public void PrintTable(string tableName, List<CodingRecord> records)
        {

            var table = new Table();
            table.Title = new TableTitle(tableName);
            table.AddColumns(
                    new TableColumn("ID").Centered(),
                    new TableColumn("Start").Centered(),
                    new TableColumn("End").Centered(),
                    new TableColumn("Duration").Centered()
            );

            if (records == null || records.Count == 0)
            {
                table.AddRow("No records in database");
            }
            else
            {
                foreach (CodingRecord record in records)
                {
                    table.AddRow(new string[]
                        {
                        record.ID.ToString(),
                        record.Start.ToString(DateTimeFormat),
                        record.End.ToString(DateTimeFormat),
                        string.Format("{0:hh} h {0:mm} m", record.Duration),
                        });
                }
            }

            AnsiConsole.Write(table);

        }
    }

}
