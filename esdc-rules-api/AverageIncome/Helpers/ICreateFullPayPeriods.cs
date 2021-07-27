using System;

using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.AverageIncome
{
    public interface ICreateFullPayPeriods
    {
        FullPayPeriod Create(PayPeriod payPeriod, DateTime startDate, DateTime endDate, DateTime minDate, DateTime maxDate);
    }
}