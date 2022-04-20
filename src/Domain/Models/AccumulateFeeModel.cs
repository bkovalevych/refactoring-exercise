namespace Domain.Models
{
    public class AccumulateFeeModel
    {
        public DateTime LastTime { get; set; }

        public int SumTax { get; set; }

        public int LastTax { get; set; }
    }
}
