using System;
using System.Collections.Generic;


namespace esdc_rules_api.AverageIncome
{
    public interface IGetIncomeList
    {
        List<decimal> Get(DateTime applicationDate, FullRoe roe);
    }
}