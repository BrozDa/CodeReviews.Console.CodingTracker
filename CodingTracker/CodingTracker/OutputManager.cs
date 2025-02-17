using Spectre.Console;
using System;

enum UserChoice
{
    ViewRecords = 1,
    AddSession,
    RemoveSession,
    UpdateSession,
    TrackSession,
    ExitApplication

}
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
    }

}
