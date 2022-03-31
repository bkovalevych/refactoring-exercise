using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Domain.Rules.DateRules.Rules
{
    public class IsHolidayRule : AbstractDateRule
    {
        public IsHolidayRule(IOptions<FreeDaySettings> options)
        {
            Holidays = options.Value.Holidays
                .Select(d => DateOnly.FromDateTime(d))
                .ToHashSet();
        }
        private HashSet<DateOnly> Holidays { get; }
        public override bool Evaluate(DateTime inputParameter)
        {
            var date = DateOnly.FromDateTime(inputParameter);
            return Holidays.Contains(date);
        }
    }
}
