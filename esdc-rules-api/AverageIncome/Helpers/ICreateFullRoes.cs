using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.AverageIncome
{
    public interface ICreateFullRoes
    {
         FullRoe Create(SimpleRoe roe);
    }
}