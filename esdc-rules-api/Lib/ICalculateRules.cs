namespace esdc_rules_api.Lib
{
    public interface ICalculateRules<T,U>
        where T : IRule
        where U : IRulePerson
    {
        decimal Calculate(T rule, U person);
    }
}