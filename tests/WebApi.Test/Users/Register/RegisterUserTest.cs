﻿using System.Globalization;
using System.Net;
using System.Text.Json;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Shouldly;
using WebApi.Test.InlineData;

namespace WebApi.Test.Users.Register
{
    public class RegisterUserTest : CashFlowClassFixture
    {

        private const string METHOD = "api/User";

        public RegisterUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await DoPost(METHOD, request);

            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
            response.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Erro_Empty_Name(string culture)
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var result = await DoPost(requestUri: METHOD, request: request, culture: culture);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray().Select(e => e.GetString()).ToList();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));
            var expectedMessage2 = ResourceErrorMessages.ResourceManager.GetString("EMAIL_INVALID", new CultureInfo(culture));

            errors.ShouldSatisfyAllConditions(
                () => errors.ShouldHaveSingleItem(),
                () => errors.ShouldContain(expectedMessage)
            );
        }
    }
}
