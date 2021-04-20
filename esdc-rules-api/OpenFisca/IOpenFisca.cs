namespace esdc_rules_api.OpenFisca
{
    public interface IOpenFisca
    {
        OpenFiscaResource Calculate(OpenFiscaResource request);
    }
}