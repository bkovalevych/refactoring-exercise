using Domain.Models;
using Domain.Rules.Calculator.Rules;
using Domain.Rules.Generic;
using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Domain.Rules.Calculator
{
    public class CalculatorRulesEngine : RulesEngine<CalculatorRule, CongestionCommand, int>
    {
        private readonly int _maxFee;
        public CalculatorRulesEngine(IServiceProvider serviceProvider, IOptions<FeeSettings> feeSettings) 
            : base(serviceProvider)
        {

            _maxFee = feeSettings.Value.MaxTaxPerDay;
        }

        public override int Execute(CongestionCommand inputParameter)
        {
            var result = Rules
                .Where(rule => rule.IsSutisfuied(inputParameter))
                .Select(rule => GroupByDateAndSum(rule, inputParameter));
            return result.Any()
                ? result.Aggregate(Reduce)
                : 0;
        }

        private int GroupByDateAndSum(CalculatorRule rule, CongestionCommand inputParameter)
        {
            var taxForAllDays = inputParameter.Dates
                .GroupBy(
                d => DateOnly.FromDateTime(d),
                (_, d) => GroupingHandler(rule, inputParameter, d));

            return taxForAllDays.Sum();
        }

        private int GroupingHandler(
            CalculatorRule rule,
            CongestionCommand inputParameter,
            IEnumerable<DateTime> d)
        {
            var grouping = new CongestionCommand()
            {
                Vehicle = inputParameter.Vehicle,
                Dates = d
            };
            return Math.Min(_maxFee,
                 rule.Evaluate(grouping));
        }

        protected override int Reduce(int one, int two)
        {
            return one + two;
        }
    }
}
