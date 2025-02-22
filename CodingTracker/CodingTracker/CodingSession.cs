namespace CodingTracker
{
    internal class CodingSession
    {
        public int Id { get; set; }
        public string Start {  get; set; }
        public string End { get; set; }
        public string Duration { get; set; }

        public DateTime StartDateTime
        {
            get => DateTime.Parse(Start);
            set => Start = value.ToString();
        } 
        public DateTime EndDateTime
        {
            get => DateTime.Parse(End);
            set => End = value.ToString();
        }
        public TimeSpan DurationTimeSpan
        {
            get => TimeSpan.Parse(Duration);
            set => Duration = value.ToString(@"hh\:mm");
        }
        public override string ToString()
        {
            return $"Id: {Id}, Start: {Start}, End: {End}, Duration: {Duration}";
        }

    }
}
