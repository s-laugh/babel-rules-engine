namespace esdc_rules_api.Lib
{
    public interface IHandleRequests<T, U> 
        where T: IRule
        where U: IRulePerson
    {
        RuleResponse Handle(RuleRequest<T,U> request);
    }
}