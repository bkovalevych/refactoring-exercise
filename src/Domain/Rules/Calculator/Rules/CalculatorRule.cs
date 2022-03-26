using Domain.Models;
using Domain.Rules.CarRules;
using Domain.Rules.FeeRules;
using Domain.Rules.Generic;

namespace Domain.Rules.Calculator.Rules
{
    public class CalculatorRule : IRule<CongestionCommand, int>
    {
        private readonly FeeRulesEngine _feeRulesEngine;
        private readonly CarRulesEngine _carRulesEngine;
        private readonly TimeSpan _maxDelay;

        public CalculatorRule()
        {
            _maxDelay = TimeSpan.FromHours(1);
            _carRulesEngine = new CarRulesEngine();
            _feeRulesEngine = new FeeRulesEngine();
        }

        public int Evaluate(CongestionCommand inputParameter)
        {
            return inputParameter.Dates
                .OrderBy(it => it)
                .Aggregate(
                    new AccumulateFeeModel(),
                    AggregateHandler, 
                    model => model.SumTax + model.LastTax);
        }

        private AccumulateFeeModel AggregateHandler(AccumulateFeeModel model, DateTime currentTime)
        {
            if (currentTime - model.LastTime > _maxDelay)
            {
                model.SumTax += model.LastTax;
                model.LastTime = currentTime;
                model.LastTax = _feeRulesEngine.Execute(currentTime);
            }
            else
            {
                model.LastTax = Math.Max(model.LastTax,
                    _feeRulesEngine.Execute(currentTime));
            }
            return model;
        }

        public bool IsSutisfuied(CongestionCommand inputParameter)
        {
            return _carRulesEngine.Execute(inputParameter.Vehicle);
        }
    }
}
