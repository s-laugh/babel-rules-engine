using esdc_rules_classes;

namespace esdc_rules_api.Lib
{
    public interface IHandleRequests<T, U> 
        where T: IRequest
        where U: IResponse
    {
        U Handle(T request);
    }
}