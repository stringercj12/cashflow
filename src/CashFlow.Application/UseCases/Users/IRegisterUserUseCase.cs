﻿using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Users
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}