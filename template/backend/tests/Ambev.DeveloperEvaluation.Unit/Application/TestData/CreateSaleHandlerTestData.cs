using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    /// <summary>
    /// Provides test data for <see cref="CreateSaleCommand"/> validation tests.
    /// </summary>
    public static class CreateSaleHandlerTestData
    {
        private static readonly Faker<CreateSaleCommand> commandFaker = new Faker<CreateSaleCommand>()
            .CustomInstantiator(f => new CreateSaleCommand(
                f.Date.Past().ToUniversalTime(),
                f.Company.CompanyName(),
                f.Random.Guid(),
                new List<CreateSaleItemCommand>
                {
                    new CreateSaleItemCommand(f.Commerce.ProductName(), f.Random.Int(1, 10), f.Random.Decimal(1, 500))
                }
            ));

        /// <summary>
        /// Generates a valid <see cref="CreateSaleCommand"/>.
        /// </summary>
        public static CreateSaleCommand GenerateValidCommand()
        {
            return commandFaker.Generate();
        }

        /// <summary>
        /// Generates an invalid <see cref="CreateSaleCommand"/> with validation errors.
        /// </summary>
        public static CreateSaleCommand GenerateInvalidCommand()
        {
            var invalidCommand = commandFaker.Generate();
            invalidCommand.SaleDate = DateTime.UtcNow.AddDays(1);
            invalidCommand.Branch = "";
            invalidCommand.CustomerId = Guid.Empty;
            return invalidCommand;
        }
    }
}
