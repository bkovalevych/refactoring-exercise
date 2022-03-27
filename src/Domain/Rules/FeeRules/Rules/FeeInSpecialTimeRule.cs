using Domain.Models;
using Domain.Rules.DateRules;
using Microsoft.Extensions.Options;

namespace Domain.Rules.FeeRules.Rules
{
    public class FeeInSpecialTimeRule : AbstractFeeRule
    {
        private readonly List<FeeSlot> _feeSlots;

        public FeeInSpecialTimeRule(DateRulesEngine dateRulesEngine, IOptions<List<FeeSlot>> feeSlots) 
            : base(dateRulesEngine)
        {
            _feeSlots = feeSlots.Value;
        }


        public override int Evaluate(DateTime inputParameter)
        {
            var time = TimeOnly.FromDateTime(inputParameter);
            var cost = _feeSlots.FirstOrDefault(x => TimeOnly.FromDateTime(x.TimeFrom) <= time 
            && time <= TimeOnly.FromDateTime(x.TimeTo))?.Cost;
            return cost ?? 0;
        }
    }
}
