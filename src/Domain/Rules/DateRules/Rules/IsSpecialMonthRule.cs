namespace Domain.Rules.DateRules.Rules
{
    public class IsSpecialMonthRule : AbstractDateRule
    {
        public HashSet<int> SpecialMonths = new HashSet<int>() { 7 };
        public override bool Evaluate(DateTime inputParameter)
        {
            return SpecialMonths.Contains(inputParameter.Month);
        }
    }
}
