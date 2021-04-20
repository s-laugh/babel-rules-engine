using System;

namespace esdc_rules_api.OpenFisca
{
    public class OpenFiscaException : Exception
    {
        public OpenFiscaException()
        {
        }

        public OpenFiscaException(string message) : base(message)
        {
        }

        public OpenFiscaException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}