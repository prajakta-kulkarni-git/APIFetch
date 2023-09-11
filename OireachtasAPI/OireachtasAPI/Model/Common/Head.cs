namespace OireachtasAPI
{
    public class Head
    {
        public Counts Counts { get; set; }
        public DateRange DateRange { get; set; }
        public string Lang { get; set; }
    }
    public class Counts
    {
        public int MemberCount { get; set; }
        public int ResultCount { get; set; }
    }

    public class DateRange
    {
        public string Start { get; set; }
        public string End { get; set; }
    }
}
