namespace CashFlow.Communication.Responses
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessages { get; set; }

        public ResponseErrorJson(string errorMessages)
        {
            ErrorMessages = [errorMessages];
        }

        public ResponseErrorJson(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
