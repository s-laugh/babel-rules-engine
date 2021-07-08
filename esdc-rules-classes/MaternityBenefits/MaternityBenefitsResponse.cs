namespace esdc_rules_classes.MaternityBenefits
{
    public class MaternityBenefitsResponse : IResponse
    {
        /// <summary>
        /// Total amount to which a Maternity benefit applicant is entitled 
        /// </summary>
        /// <value></value>
        public decimal Amount { get; set; }
    }
}