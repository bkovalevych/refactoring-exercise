namespace Domain.Rules.Generic
{
    public interface IRule<TInputParameter, TOutputParameter>
    {
        bool IsSutisfuied(TInputParameter inputParameter);

        TOutputParameter Evaluate(TInputParameter inputParameter); 
    }
}
