using System;
using System.Collections.Generic;


namespace esdc_rules_api.AverageIncome
{
    public interface IGetIncomeFromOneRoe
    {
        decimal Get(FullPayPeriod payPeriod, DateTime startOfWeek, DateTime minDate, DateTime maxDate);
    }
}