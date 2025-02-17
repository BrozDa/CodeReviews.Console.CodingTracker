
namespace CodingTracker
{
    /// <summary>
    /// Class represeting record in the database
    /// </summary>
    internal class CodingRecord
    {
        public int ID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Duration { get; set; }
        public CodingRecord()
        {
            //setup default values
            ID = 0;
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            Duration = TimeSpan.Zero;
        }
        public CodingRecord(int id, DateTime start, DateTime end)
        {
            ID = id;
            Start = start;
            End = end;
            Duration = start - end;
        }
        public CodingRecord(int id, DateTime end, DateTime start, TimeSpan duration)
        {
            ID = id;
            Start = start;
            End = end;
            Duration = duration;
        }

    }
}
