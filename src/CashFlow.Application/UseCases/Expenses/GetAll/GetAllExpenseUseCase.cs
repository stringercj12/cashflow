using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpenseUseCase : IGetAllExpenseUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public GetAllExpenseUseCase(
            IExpensesReadOnlyRepository repository,
            ILoggedUser loggedUser,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseExpensesJson> Execute()
        {

            var loggedUser = await _loggedUser.Get();

            var result = await _repository.GetAll(loggedUser);

            return new ResponseExpensesJson
            {
                Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
            };

        }
    }
}



