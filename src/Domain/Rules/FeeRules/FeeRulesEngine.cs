using Domain.Rules.FeeRules.Rules;
using Domain.Rules.Generic;

namespace Domain.Rules.FeeRules
{
    public class FeeRulesEngine : RulesEngine<AbstractFeeRule, DateTime, int>
    {
        protected override int Reduce(int one, int two)
        {
            return one + two;
        }
    }
}
