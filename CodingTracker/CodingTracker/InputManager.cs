using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodingTracker
{
    /// <summary>
    /// Manages all input handling and verification of said input
    /// </summary>
    internal class InputManager
    {
        public CodingRecord GetNewRecord()
        {
            DateTime start = GetStartTime();
            DateTime end = GetEndTime(start);
            return new CodingRecord();
        }
        public DateTime GetStartTime()
        {
            DateTime startdate = DateTime.Now;

            var start = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter start time in format DD-MM-YYY HH:MM")
            .DefaultValue(DateTime.Now.ToString("dd-MM-yyyy HH:mm"))
                .Validate(
                    (input) => DateTime.TryParseExact(input, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startdate)
                ));

            return startdate;
        }
        public DateTime GetEndTime(DateTime start)
        {
            DateTime endDate = DateTime.Now;

            var end = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter start time in format DD-MM-YYY HH:MM")
            .DefaultValue(DateTime.Now.ToString("dd-MM-yyyy HH:mm"))
                .Validate(
                    (input) => (DateTime.TryParseExact(input, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate) 
                                && endDate.Subtract(start) > TimeSpan.Zero)
                ));

            return endDate;
        }
    }
}
