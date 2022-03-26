using Domain.Rules.Generic;
using Domain.Vehicles;

namespace Domain.Rules.CarRules.Rules
{
    public abstract class AbstractCarRule : IRule<IVehicle, bool>
    {
        public abstract bool Evaluate(IVehicle inputParameter);

        public bool IsSutisfuied(IVehicle inputParameter)
        {
            return true;
        }
    }
}
