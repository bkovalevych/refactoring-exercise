using Domain.Rules.Generic;

namespace Domain.Rules.DateRules.Rules
{
    public abstract class AbstractDateRule : IRule<DateTime, bool>
    {
        public abstract bool Evaluate(DateTime inputParameter);

        public bool IsSutisfuied(DateTime inputParameter)
        {
            return true;
        }
    }
}
