using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for <see cref="SaleItem"/>.
/// This ensures consistency across test cases and provides valid and invalid scenarios.
/// </summary>
public static class SaleItemTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .CustomInstantiator(f => new SaleItem(
            f.Random.Guid(),
            f.Commerce.ProductName(),
            f.Random.Int(1, 10),
            f.Random.Decimal(10, 500),
            f.Random.Decimal(0, 50)
        ));

    /// <summary>
    /// Generates a valid SaleItem entity with randomized data.
    /// </summary>
    /// <returns>A valid SaleItem entity.</returns>
    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates a SaleItem with no discount.
    /// </summary>
    public static SaleItem GenerateSaleItemWithoutDiscount()
    {
        return new SaleItem(
            Guid.NewGuid(),
            "Gaming Mouse",
            2,
            200m,
            0m
        );
    }

    /// <summary>
    /// Generates an invalid SaleItem entity.
    /// </summary>
    /// <returns>An invalid SaleItem entity.</returns>
    public static SaleItem GenerateInvalidSaleItem()
    {
        return new SaleItem(
            Guid.Empty,
            "",
            0,
            -10m,
            -5m
        );
    }
}
