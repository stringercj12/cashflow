using CashFlow.Domain.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UnitOfWOrkBuilder
    {
        public static IUnitOfWork Build()
        {
            var mock = new Mock<IUnitOfWork>();

            return mock.Object;
        }
    }
}
