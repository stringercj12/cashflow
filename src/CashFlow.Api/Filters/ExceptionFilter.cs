﻿using CashFlow.Communication.Responses;
using CashFlow.Exception;
using ClashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CashFlowException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnkowError(context);
            }

        }

        private void HandleProjectException(ExceptionContext context)
        {

            if (context.Exception is ErrorOnValidationException)
            {
                var ex = (ErrorOnValidationException)context.Exception;

                var errorResponse = new ResponseErrorJson(ex.Errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);

            }
        }

        private void ThrowUnkowError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
