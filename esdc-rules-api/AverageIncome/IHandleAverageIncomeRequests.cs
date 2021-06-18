namespace esdc_rules_api.AverageIncome
{
    public interface IHandleAverageIncomeRequests
    {
        AverageIncomeResponse Handle(AverageIncomeRequest request);
    }
}