using System;
using System.Collections.Generic;

using esdc_rules_api.Lib;
using esdc_rules_classes.AverageIncome;
using ppTypes = esdc_rules_api.AverageIncome.ValidPayPeriodTypes;


namespace esdc_rules_api.AverageIncome
{
    public class AverageIncomeRequestValidator : IValidateRequests<AverageIncomeRequest>
    {
        private readonly List<string> _validPayPeriodTypes = new List<string>() {
            ppTypes.WEEKLY, ppTypes.BIWEEKLY, ppTypes.SEMIMONTHLY, ppTypes.MONTHLY
        };

        public void Validate(AverageIncomeRequest request) {
            if (request.ApplicationDate > DateTime.Now.AddDays(1)) {
                throw new ValidationException("Application date cannot be in the future");
            }

            // TODO: We seem to be encountering a few of these. 
            // Look into ensuring the flow works if this is the case, and removing this exception
            if (request.ApplicationDate < request.Roe.LastDayForWhichPaid) {
                throw new ValidationException("Application date must be after last date for which paid");
            }

            if (request.NumBestWeeks < 1 || request.NumBestWeeks > 52) {
                throw new ValidationException("Number of Best Weeks must be between 0 and 52");
            }

            if (request.Roe.PayPeriods.Count < 1) {
                throw new ValidationException("At least one pay period is required on the Record of Employment");
            }

            if (request.Roe.FinalPayPeriodDay < request.Roe.LastDayForWhichPaid) {
                throw new ValidationException("Final pay period day must be later than the last day for which paid");
            }

            if (request.Roe.LastDayForWhichPaid < request.Roe.FirstDayForWhichPaid) {
                throw new ValidationException("First day for which paid must be before last day for which paid");
            }

            if (!_validPayPeriodTypes.Contains(request.Roe.PayPeriodType)) {
                string validTypes = String.Join(", ", _validPayPeriodTypes);
                throw new ValidationException($"Invalid pay period type. Must be one of: {validTypes}");
            }
        }
    }
}