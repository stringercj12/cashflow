namespace ClashFlow.Exception.ExceptionsBase
{
    public abstract class CashFlowException : SystemException
    {
        protected CashFlowException(string message) : base(message)
        {

        }

        public abstract int StatucCode { get; }
        public abstract List<string> GetErrors();
    }
}
