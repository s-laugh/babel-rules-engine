using System;

namespace esdc_rules_api.AverageIncome
{
    public interface IGetMainStartDate
    {
        DateTime Get(DateTime firstDayForWhichPaid, DateTime applicationDate);
    }
}