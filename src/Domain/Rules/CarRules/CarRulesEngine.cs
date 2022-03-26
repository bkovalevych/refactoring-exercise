using Domain.Rules.CarRules.Rules;
using Domain.Rules.Generic;
using Domain.Vehicles;

namespace Domain.Rules.CarRules
{
    public class CarRulesEngine : RulesEngine<AbstractCarRule, IVehicle, bool>
    {
        protected override bool Reduce(bool one, bool two)
        {
            return one || two;
        }
    }
}
