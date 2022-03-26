using Domain.Vehicles;

namespace Domain.Models
{
    public class CongestionCommand
    {
        public IEnumerable<DateTime> Dates { get; set; }

        public IVehicle Vehicle { get; set; }
    }
}
