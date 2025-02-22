
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace CodingTracker
{
    /// <summary>
    /// Class represeting record in the database
    /// </summary>
    internal class CodingRecord
    {
        public int ID { get; set; }

        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public string DurationString { get; set; }

        public DateTime Start 
        {
            get => DateTime.Parse(StartDateString);
            set => StartDateString = value.ToString(); 
        }
        public DateTime End
        {
            get => DateTime.Parse(EndDateString);
            set => EndDateString = value.ToString();
        }
        public TimeSpan Duration
        {
            get => TimeSpan.Parse(DurationString);
            set => DurationString = value.ToString(@"hh\:mm");
        }
        public CodingRecord(long ID, string Start, string End, string Duration)
        {
            this.ID = (int)ID; // Explicit conversion
            this.StartDateString = Start;
            this.EndDateString = End;
            this.DurationString = Duration;
        }
        public CodingRecord(DateTime start, DateTime end, TimeSpan duration)
        {
            this.StartDateString = start.ToString();
            this.EndDateString = end.ToString();
            this.DurationString = duration.ToString(@"hh\:mm");
 
        }

    }
}
