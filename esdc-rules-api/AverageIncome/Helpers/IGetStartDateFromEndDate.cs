using System;

namespace esdc_rules_api.AverageIncome
{
    public interface IGetStartDateFromEndDate
    {
        DateTime Get(DateTime endDate, string ppType);

    }
}