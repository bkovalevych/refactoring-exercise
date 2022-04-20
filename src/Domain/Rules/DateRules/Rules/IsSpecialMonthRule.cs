using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Domain.Rules.DateRules.Rules
{
    public class IsSpecialMonthRule : AbstractDateRule
    {
        private readonly HashSet<int> _freeMonths;
        public IsSpecialMonthRule(IOptions<FreeDaySettings> options)
        {
            _freeMonths = options.Value
                .FreeMonths
                .ToHashSet();
        }
        public override bool Evaluate(DateTime inputParameter)
        {
            return _freeMonths.Contains(inputParameter.Month);
        }
    }
}
