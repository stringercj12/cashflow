﻿using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCase.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            // TO DO VALIDATIONS

            Validate(request);

            return new ResponseRegisteredExpenseJson();
        }

        private void Validate(RequestRegisterExpenseJson request)
        {
            var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);

            if(titleIsEmpty)
            {
                throw new ArgumentException("The title is required");
            }

            if(request.Amount <= 0)
            {
                throw new ArgumentException("The amount must be than zero.");
            }

            var result = DateTime.Compare(request.Date, DateTime.UtcNow);

            if(result > 0)
            {
                throw new ArgumentException("Expenses cannot be for the future");
            }

           var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);

            if (paymentTypeIsValid == false)
            {
                throw new ArgumentException("Payment Type is not valid.");
            }
        }
    }
}
