using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    public class ExpensesController : Controller
    {

        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
        {
            var useCase = new RegisterExpenseUseCase();
            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
