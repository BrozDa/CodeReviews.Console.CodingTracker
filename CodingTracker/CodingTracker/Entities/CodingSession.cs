namespace CodingTracker
{
    internal class CodingSession
    {
        private DateTime _start;
        private DateTime _end;
        public int Id { get; set; }
        public DateTime Start
        {
            get {  return _start; }
            set { _start = value.ToUniversalTime(); } 
        }
        public DateTime End
        {
            get { return _end; }
            set { _end = value.ToUniversalTime(); }
        }
        public TimeSpan Duration
        { 
            get => End - Start;
        }

    }
}
