﻿using System.Net.Mime;
using CashFlow.Application.UseCase.Expenses.Reports.Excel;
using CashFlow.Application.UseCase.Expenses.Reports.Pdf;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcel([FromServices] IGenerateExpensesReportExcelUseCase useCase, [FromHeader] DateOnly month)
        {
            byte[] file = await useCase.Execute(month);

            if (file.Length > 0)
                return File(file, MediaTypeNames.Application.Octet, "report.xlsx");


            return NoContent();

        }

        
        [HttpGet("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPdf([FromServices] IGenerateExpensesReportPdfUseCase useCase, [FromQuery] DateOnly month)
        {
            byte[] file = await useCase.Execute(month);

            if (file.Length > 0)
                return File(file, MediaTypeNames.Application.Pdf, "report.pdf");


            return NoContent();

        }
    }
}
