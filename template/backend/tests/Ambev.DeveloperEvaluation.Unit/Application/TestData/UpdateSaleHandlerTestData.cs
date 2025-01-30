using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides test data for <see cref="UpdateSaleCommand"/> validation tests.
    /// </summary>
    public static class UpdateSaleHandlerTestData
    {
        private static readonly Faker<UpdateSaleCommand> commandFaker = new Faker<UpdateSaleCommand>()
            .CustomInstantiator(f => new UpdateSaleCommand(
                f.Random.Guid(),
                f.Random.Bool(),
                new List<UpdateSaleItemCommand>
                {
                    new UpdateSaleItemCommand(f.Random.Guid(), f.Commerce.ProductName(), f.Random.Int(1, 10), f.Random.Decimal(1, 500), false)
                }
            ));

        /// <summary>
        /// Generates a valid <see cref="UpdateSaleCommand"/>.
        /// </summary>
        public static UpdateSaleCommand GenerateValidCommand()
        {
            return commandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid <see cref="UpdateSaleCommand"/> with validation errors.
        /// </summary>
        public static UpdateSaleCommand GenerateInvalidCommand()
        {
            var invalidCommand = commandFaker.Generate();
            invalidCommand.Id = Guid.Empty;
            invalidCommand.Items.Clear();
            return invalidCommand;
        }
    }
}
