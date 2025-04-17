﻿using CashFlow.Application.UseCases.Users;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Test.Users.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        public void Error_Name_Empty(string name)
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = name;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldSatisfyAllConditions(
                error => error.ShouldHaveSingleItem(),
                error => error.ShouldNotBeNull(),
                error => error.ShouldContain(m => m.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY))
            );
        }


        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        public void Error_Email_Empty(string email)
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = email;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldSatisfyAllConditions(
                error => error.ShouldHaveSingleItem(),
                error => error.ShouldNotBeNull(),
                error => error.ShouldContain(m => m.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_EMPTY))
            );
        }


        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "jefferson.com";

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldSatisfyAllConditions(
                error => error.ShouldHaveSingleItem(),
                error => error.ShouldNotBeNull(),
                error => error.ShouldContain(m => m.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID))
            );
        }

        [Fact]
        public void Error_Password_Empty()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Password = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldSatisfyAllConditions(
                error => error.ShouldHaveSingleItem(),
                error => error.ShouldNotBeNull(),
                error => error.ShouldContain(m => m.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD))
            );
        }

    }
}
