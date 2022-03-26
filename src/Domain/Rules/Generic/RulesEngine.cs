using System.Reflection;

namespace Domain.Rules.Generic
{
    public abstract class RulesEngine<TRule, TInputParameter, TOutputParameter>
        where TRule : IRule<TInputParameter, TOutputParameter>
    {
        public RulesEngine()
        {
            Rules = Assembly.GetAssembly(typeof(TRule))
                .GetTypes()
                .Where(t =>
                {
                    var r = !t.IsAbstract && typeof(TRule).IsAssignableFrom(t);   
                    return r;
                })
                .Select(t => {
                    
                    return (TRule)Activator.CreateInstance(t);
                })
                .ToList();
        }

        public virtual TOutputParameter Execute(TInputParameter inputParameter)
        {
            var appropriateRules = Rules.Where(rule => rule.IsSutisfuied(inputParameter));
            if (!appropriateRules.Any()) 
            { 
                return default; 
            }
            var result = appropriateRules
                .Select(rule => rule.Evaluate(inputParameter))
                .Aggregate(Reduce);
            return result;
        }

        protected List<TRule> Rules { get; } = new List<TRule>();
        protected abstract TOutputParameter Reduce(TOutputParameter one, TOutputParameter two);

    }
}
