using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for <see cref="Sale"/>.
/// This ensures consistency across test cases and provides valid and invalid scenarios.
/// </summary>
public static class SaleTestData
{
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .CustomInstantiator(f => new Sale(
            f.Date.Past(),
            f.Company.CompanyName(),
            f.Random.Guid()
        ));

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// </summary>
    /// <returns>A valid Sale entity.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Generates a sale with no items for testing cancellation scenarios.
    /// </summary>
    public static Sale GenerateSaleWithoutItems()
    {
        var sale = SaleFaker.Generate();
        return sale;
    }

    /// <summary>
    /// Generates an invalid Sale entity.
    /// </summary>
    /// <returns>An invalid Sale entity.</returns>
    public static Sale GenerateInvalidSale()
    {
        return new Sale(DateTime.UtcNow, "", Guid.Empty);
    }
}
