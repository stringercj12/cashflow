﻿using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncrypterBuilder
    {
        private readonly Mock<IPasswordEncrypter> _mock;

        public PasswordEncrypterBuilder()
        {
            _mock = new Mock<IPasswordEncrypter>();
            _mock.Setup(passwordEncripter => passwordEncripter.Encrypt(It.IsAny<string>()))
                .Returns("encryptedPassword");
        }

        public PasswordEncrypterBuilder Verify(string? password = null)
        {
            if(string.IsNullOrWhiteSpace(password) == false)
            {
                _mock.Setup(passwordEncripter => passwordEncripter.Verify(password, It.IsAny<string>()))
                    .Returns(true);
            }

            return this;
        }

        public IPasswordEncrypter Build() => _mock.Object;
    }
}
