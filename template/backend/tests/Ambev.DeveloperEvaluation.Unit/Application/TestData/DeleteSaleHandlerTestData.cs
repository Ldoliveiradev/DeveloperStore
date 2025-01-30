using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides test data for <see cref="DeleteSaleCommand"/> validation tests.
    /// </summary>
    public static class DeleteSaleHandlerTestData
    {
        private static readonly Faker<DeleteSaleCommand> commandFaker = new Faker<DeleteSaleCommand>()
            .CustomInstantiator(f => new DeleteSaleCommand(f.Random.Guid()));

        /// <summary>
        /// Generates a valid <see cref="DeleteSaleCommand"/>.
        /// </summary>
        public static DeleteSaleCommand GenerateValidCommand()
        {
            return commandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid <see cref="DeleteSaleCommand"/> with validation errors.
        /// </summary>
        public static DeleteSaleCommand GenerateInvalidCommand()
        {
            return new DeleteSaleCommand(Guid.Empty);
        }
    }
}
