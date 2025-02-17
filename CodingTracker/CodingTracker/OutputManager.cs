using Spectre.Console;
using System;


namespace CodingTracker
{
    /// <summary>
    /// Manages all output to user's screen
    /// </summary>
    internal class OutputManager
    {
        public void PrintMenuHeader()
        {
            Console.WriteLine("Welcome to Coding Tracker application");
            Console.WriteLine("This application allow you to manually add sessions from past or track a session as it happen");
            Console.WriteLine();
        }

        public UserChoice PrintMenu()
        {
            UserChoice[] menuOptions = (UserChoice[])Enum.GetValues(typeof(UserChoice));//{UserChoice.ViewRecords, UserChoice.AddSession, UserChoice.RemoveSession, UserChoice.UpdateSession, UserChoice.TrackSession};

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
                }));

            return input;
        }
        public void PrintTable(string tableName, List<CodingRecord> records)
        {
            var table = new Table();
            table.Title = new TableTitle(tableName);

            table.AddColumns(new string[] {"ID", "Start", "End", "Duration"}).Centered();


            foreach (CodingRecord record in records)
            {
                table.AddRow(new string[] { record.ID.ToString(), record.Start.ToString(), record.End.ToString(), record.Duration.ToString() });
            }
            AnsiConsole.Write(table);
        }
    }

}
