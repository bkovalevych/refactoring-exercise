namespace Domain.Rules.DateRules.Rules
{
    public class IsWeekEndRule : AbstractDateRule
    {
        public override bool Evaluate(DateTime inputParameter)
        {
            return inputParameter.DayOfWeek == DayOfWeek.Sunday
                || inputParameter.DayOfWeek == DayOfWeek.Saturday;
        }
    }
}
