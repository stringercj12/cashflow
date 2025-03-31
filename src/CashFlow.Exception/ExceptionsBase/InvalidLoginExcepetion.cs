
using System.Net;

namespace CashFlow.Exception.ExceptionsBase
{
    public class InvalidLoginExcepetion : CashFlowException
    {
        public InvalidLoginExcepetion() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
        {

        }

        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
