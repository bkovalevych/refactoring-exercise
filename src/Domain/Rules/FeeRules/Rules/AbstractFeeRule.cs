using Domain.Rules.DateRules;
using Domain.Rules.Generic;

namespace Domain.Rules.FeeRules.Rules
{
    public abstract class AbstractFeeRule : IRule<DateTime, int>
    {
        public readonly DateRulesEngine _dateRulesEngine;

        public AbstractFeeRule()
        {
            _dateRulesEngine = new DateRulesEngine();
        }

        public abstract int Evaluate(DateTime inputParameter);

        public bool IsSutisfuied(DateTime inputParameter)
        {
            return _dateRulesEngine.Execute(inputParameter);
        }
    }
}
