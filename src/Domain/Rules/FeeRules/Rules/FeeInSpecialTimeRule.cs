using Domain.Models;
using Domain.Rules.DateRules;
using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Domain.Rules.FeeRules.Rules
{
    public class FeeInSpecialTimeRule : AbstractFeeRule
    {
        private readonly List<FeeSlot> _feeSlots;

        public FeeInSpecialTimeRule(DateRulesEngine dateRulesEngine, IOptions<FeeSettings> feeSlots) 
            : base(dateRulesEngine)
        {
            _feeSlots = feeSlots.Value.FeeSlots;
        }


        public override int Evaluate(DateTime inputParameter)
        {
            var cost = _feeSlots.FirstOrDefault(
                x => FindHandler(x, inputParameter))?.Cost;
            return cost ?? 0;
        }

        private bool FindHandler(FeeSlot slot, DateTime time)
        {
            var from = slot.TimeFrom;
            var preparedTime = new DateTime(
                from.Year, 
                from.Month,
                from.Day,
                time.Hour, 
                time.Minute,
                0);
            var timeTo = slot.TimeTo;
            if(from > timeTo)
            {
                timeTo = timeTo.AddDays(1);
            }
            return from <= preparedTime && preparedTime <= timeTo;
        }
    }
}
