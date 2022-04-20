namespace Domain.Settings
{
    public class FreeDaySettings
    {
        public List<int> FreeDaysOfWeek { get; set; } = new();

        public List<DateTime> Holidays { get; set; } = new();

        public List<int> FreeMonths { get; set; } = new();
    }
}
