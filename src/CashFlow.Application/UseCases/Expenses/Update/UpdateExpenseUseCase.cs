﻿using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Exception;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public class UpdateExpenseUseCase : IUpdateExpenseUseCase
    {
        private readonly IExpensesUpdateOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;

        public UpdateExpenseUseCase(
            IExpensesUpdateOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILoggedUser loggedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }
        public async Task Execute(long id, RequestExpenseJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.Get();

            var expense = await _repository.GetById(loggedUser, id);

            if (expense is null)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSES_NOT_FOUND);
            }

            _mapper.Map(request, expense);

            _repository.Update(expense);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestExpenseJson request)
        {
            var validator = new ExpenseValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
