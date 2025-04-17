using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Test.Expenses.Register
{
    public class RegisterExpenseValidatorsTests
    {
        [Fact]
        public void Success()
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldNotBe(false);
            //result.Tokens.ShouldBeNotNull();
            //result.Name.ShouldBe(request.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void Error_Title_Empty(string title)
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Title = title;

            var result = validator.Validate(request);

            result.IsValid.ShouldBe(false);
            Console.Write(result.Errors);
            result.Errors.ShouldSatisfyAllConditions(
               errors => errors.ShouldHaveSingleItem(),
               errors => errors.ShouldNotBeNull(),
               errors => errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED))
            );
        }


        [Fact]
        public void Error_Date_Future()
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            var result = validator.Validate(request);

            result.IsValid.ShouldBe(false);
            Console.Write(result.Errors);
            result.Errors.ShouldSatisfyAllConditions(
               errors => errors.ShouldHaveSingleItem(),
               errors => errors.ShouldNotBeNull(),
               errors => errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_FOR_TH_FUTURE))
            );
        }


        [Fact]
        public void Error_Payment_Type_Invalid()
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.PaymentType = (PaymentType)700;

            var result = validator.Validate(request);

            result.IsValid.ShouldBe(false);
            Console.Write(result.Errors);
            result.Errors.ShouldSatisfyAllConditions(
               errors => errors.ShouldHaveSingleItem(),
               errors => errors.ShouldNotBeNull(),
               errors => errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID))
            );
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-7)]
        public void Error_Amount_Invalid(decimal amount)
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Amount = amount;

            var result = validator.Validate(request);

            result.IsValid.ShouldBe(false);
            Console.Write(result.Errors);
            result.Errors.ShouldSatisfyAllConditions(
               errors => errors.ShouldHaveSingleItem(),
               errors => errors.ShouldNotBeNull(),
               errors => errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO))
            );
        }
    }
}
