namespace CodingTracker
{
    internal class CodingSession
    {
        public int Id { get; set; }
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        public TimeSpan Duration
        { 
            get => End - Start;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Start: {Start}, End: {End}, Duration: {Duration}";
        }

    }
}
