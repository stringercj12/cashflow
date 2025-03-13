using System.Net;

namespace CashFlow.Exception.ExceptionsBase
{
    public class ErrorOnValidationException : CashFlowException
    {
        private readonly List<string> _errors;

        public override int StatucCode => (int)HttpStatusCode.BadRequest;

        public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
        {
            _errors = errorMessages;
        }

        public override List<string> GetErrors()
        {
            return _errors;
        }
    }
}
