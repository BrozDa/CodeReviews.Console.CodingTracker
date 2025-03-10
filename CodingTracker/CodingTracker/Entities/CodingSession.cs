namespace CodingTracker
{
    internal class CodingSession
    {
        private DateTime _start;
        private DateTime _end;
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public TimeSpan Duration
        {
            get => End - Start;
        }
        
    }
}