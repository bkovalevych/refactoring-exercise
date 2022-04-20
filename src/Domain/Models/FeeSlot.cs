namespace Domain.Models
{
    public class FeeSlot
    {
        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public int Cost { get; set; }
    }
}
