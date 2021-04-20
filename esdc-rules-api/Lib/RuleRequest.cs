namespace esdc_rules_api.Lib
{
    public class RuleRequest<T,U>
        where T: IRule
        where U: IRulePerson
    {
        public T Rule { get; set; }
        public U Person {get; set; }
    }
}