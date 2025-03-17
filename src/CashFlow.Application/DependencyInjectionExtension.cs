using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCase.Expenses.Delete;
using CashFlow.Application.UseCase.Expenses.GetAll;
using CashFlow.Application.UseCase.Expenses.GetById;
using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Application.UseCase.Expenses.Reports.Excel;
using CashFlow.Application.UseCase.Expenses.Reports.Pdf;
using CashFlow.Application.UseCase.Expenses.Update;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddUseCases(services);
            AddAutoMapper(services);
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
            services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
            services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
            services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
            services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
            services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
            services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
        }
    }
}
