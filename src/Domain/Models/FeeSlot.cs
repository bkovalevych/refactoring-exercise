namespace Domain.Models
{
    public class FeeSlot
    {
        public TimeOnly TimeFrom { get; set; }

        public TimeOnly TimeTo { get; set; }

        public int Cost { get; set; }
    }
}
