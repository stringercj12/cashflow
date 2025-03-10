using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCase.Expenses.GetById
{
    public interface IGetExpenseByIdUseCase
    {
        Task<ResponseExpenseJson> Execute(long id);
    }
}
