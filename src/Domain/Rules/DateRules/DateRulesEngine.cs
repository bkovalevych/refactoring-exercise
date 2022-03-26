using Domain.Rules.DateRules.Rules;
using Domain.Rules.Generic;

namespace Domain.Rules.DateRules
{
    public class DateRulesEngine : RulesEngine<AbstractDateRule, DateTime, bool>
    {
        public override bool Execute(DateTime inputParameter)
        {
            return !base.Execute(inputParameter);
        }

        protected override bool Reduce(bool one, bool two)
        {
            return one || two;
        }
    }
}
