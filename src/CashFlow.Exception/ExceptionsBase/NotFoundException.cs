using System.Net;

namespace CashFlow.Exception.ExceptionsBase
{
    public class NotFoundException : CashFlowException
    {
        public NotFoundException(string message) : base(message)
        {

        }

        public override int StatucCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
