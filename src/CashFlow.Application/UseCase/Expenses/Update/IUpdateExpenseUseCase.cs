using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCase.Expenses.Update
{
    public interface IUpdateExpenseUseCase
    {
        Task Execute(long id, RequestExpenseJson request);
    }
}
