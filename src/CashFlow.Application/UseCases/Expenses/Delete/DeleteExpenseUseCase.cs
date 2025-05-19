
using AutoMapper;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase : IDeleteExpenseUseCase
    {
        private readonly IExpensesReadOnlyRepository _expenseReadOnlyRepository;
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;

        public DeleteExpenseUseCase(
            IExpensesWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            ILoggedUser loggedUser,
            IExpensesReadOnlyRepository expenseReadOnlyRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
            _expenseReadOnlyRepository = expenseReadOnlyRepository;
        }

        public async Task Execute(long id)
        {

            var loggedUser = await _loggedUser.Get();

            var expense = _expenseReadOnlyRepository.GetById(loggedUser, id);

            if (expense is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSES_NOT_FOUND);
            }

            await _repository.Delete(id);

            await _unitOfWork.Commit();
        }
    }
}

