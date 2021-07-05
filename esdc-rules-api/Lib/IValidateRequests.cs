using esdc_rules_classes;

namespace esdc_rules_api.Lib
{
    public interface IValidateRequests<T> 
        where T: IRequest
    {
        void Validate(T request);
    }
}