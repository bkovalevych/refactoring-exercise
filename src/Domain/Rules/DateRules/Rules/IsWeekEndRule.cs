using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Domain.Rules.DateRules.Rules
{
    public class IsWeekEndRule : AbstractDateRule
    {
        private readonly HashSet<int> _freeDaysOfWeek;
        public IsWeekEndRule(IOptions<FreeDaySettings> options)
        {
            _freeDaysOfWeek = options.Value.FreeDaysOfWeek.ToHashSet();
        }

        public override bool Evaluate(DateTime inputParameter)
        {
            return _freeDaysOfWeek.Contains((int)inputParameter.DayOfWeek);
        }
    }
}
