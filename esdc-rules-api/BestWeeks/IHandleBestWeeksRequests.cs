namespace esdc_rules_api.BestWeeks
{
    public interface IHandleBestWeeksRequests
    {
        BestWeeksResponse Handle(BestWeeksRequest request);
    }
}