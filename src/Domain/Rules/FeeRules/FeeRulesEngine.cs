using Domain.Rules.FeeRules.Rules;
using Domain.Rules.Generic;

namespace Domain.Rules.FeeRules
{
    public class FeeRulesEngine : RulesEngine<AbstractFeeRule, DateTime, int>
    {
        public FeeRulesEngine(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        protected override int Reduce(int one, int two)
        {
            return one + two;
        }
    }
}
