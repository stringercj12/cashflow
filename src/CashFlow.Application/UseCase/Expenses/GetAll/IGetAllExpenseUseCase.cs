using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCase.Expenses.GetAll
{
    public interface IGetAllExpenseUseCase
    {
        Task<ResponseExpensesJson> Execute();
    }
}
