using CashFlow.Application.UseCases.Login;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using Shouldly;

namespace UseCases.Test.Login
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();

            var request = RequestLoginJsonBuilder.Build();
            request.Email = user.Email;

            var useCase = CreateUseCase(user, request.Password);

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(user.Name);
            result.Token.ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_User_Not_Found()
        {
            var user = UserBuilder.Build();

            var request = RequestLoginJsonBuilder.Build();

            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<InvalidLoginExcepetion>();

            result.GetErrors().ShouldHaveSingleItem().ShouldContain(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID);
        }

        [Fact]
        public async Task Error_Password_Not_Match()
        {
            var user = UserBuilder.Build();

            var request = RequestLoginJsonBuilder.Build();
            request.Email = user.Email;

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<InvalidLoginExcepetion>();

            result.GetErrors().ShouldHaveSingleItem().ShouldContain(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID);
        }

        private IDoLoginUseCase CreateUseCase(User user, string? password = null)
        {
            var passwordEncripter = new PasswordEncrypterBuilder().Verify(password).Build();
            var tokenGenerator = JwtTokenGeneratorBuilder.Build();
            var readRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();

            return new DoLoginUseCase(readRepository, passwordEncripter, tokenGenerator);
        }
    }
}
