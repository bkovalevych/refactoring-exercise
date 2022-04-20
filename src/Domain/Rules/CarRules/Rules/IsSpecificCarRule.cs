using Domain.Vehicles;

namespace Domain.Rules.CarRules.Rules
{
    public class IsSpecificCarRule : AbstractCarRule
    {
        public override bool Evaluate(IVehicle inputParameter)
        {
            return inputParameter?.IsFree ?? false;
        }
    }
}
