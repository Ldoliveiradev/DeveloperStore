using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides test data for <see cref="GetSaleCommand"/> validation tests.
    /// </summary>
    public static class GetSaleHandlerTestData
    {
        private static readonly Faker<GetSaleCommand> commandFaker = new Faker<GetSaleCommand>()
            .CustomInstantiator(f => new GetSaleCommand(f.Random.Guid()));

        /// <summary>
        /// Generates a valid <see cref="GetSaleCommand"/>.
        /// </summary>
        public static GetSaleCommand GenerateValidCommand()
        {
            return commandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid <see cref="GetSaleCommand"/> with validation errors.
        /// </summary>
        public static GetSaleCommand GenerateInvalidCommand()
        {
            return new GetSaleCommand(Guid.Empty);
        }
    }
}
