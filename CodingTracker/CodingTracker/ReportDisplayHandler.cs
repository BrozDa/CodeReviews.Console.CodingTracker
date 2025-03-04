using Spectre.Console;

namespace CodingTracker
{
    internal class ReportDisplayHandler
    {


        public ReportChoice GetTimeFrame()
        {
            var reportChoices = Enum.GetValues<ReportChoice>();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<ReportChoice>()
                .Title("Please choose timeframe for the report")
                .UseConverter((x) => x switch
                { 
                    ReportChoice.ThisYear => "This Year",
                    ReportChoice.ThisMonth => "This Month",
                    ReportChoice.ThisWeek => "This Week",
                    ReportChoice.Custom => "Custom Range",
                    ReportChoice.Exit => "Exit to menu",
                })
                .AddChoices(reportChoices)
             );
            return choice;
        }
        public bool ShouldBeOutputAsceding()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<bool>()
                .Title("Please confirm if output of the report should be in ascending or descending order")
                .AddChoices(true, false)
                .UseConverter((choice) => choice ? "Ascending" : "Descending")
                );
            
            return choice;
        }
    }
}
