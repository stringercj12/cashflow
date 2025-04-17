using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Login
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IPasswordEncrypter _passwordEncripter;

        public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncrypter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _repository = repository;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var user = await _repository.GetUserByEmail(request.Email);

            if (user is null)
            {
                throw new InvalidLoginExcepetion();
            }

            var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

            if (passwordMatch == false)
            {
                throw new InvalidLoginExcepetion();
            }

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Token = _accessTokenGenerator.Generate(user)
            };
        }
    }
}
