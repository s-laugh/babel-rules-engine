using esdc_rules_classes.MaternityBenefits;

namespace esdc_rules_api.MaternityBenefits
{
    public interface IHandleBulkRequests
    {
         MaternityBenefitsBulkResponse Handle(MaternityBenefitsBulkRequest request);
    }
}