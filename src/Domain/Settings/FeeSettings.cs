using Domain.Models;

namespace Domain.Settings
{
    public class FeeSettings
    {
        public int MaxDelayForSingleTaxInMinutes { get; set; }

        public int MaxTaxPerDay { get; set; }

        public List<FeeSlot> FeeSlots { get; set; } = new(); 
    }
}
