using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Unit tests for the <see cref="Sale"/> entity.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that a valid sale is created successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then sale is created successfully")]
    public void Given_ValidSaleData_When_CreatingSale_Then_SaleIsCreatedSuccessfully()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Assert
        sale.Should().NotBeNull();
        sale.SaleDate.Should().BeBefore(DateTime.UtcNow);
        sale.CustomerId.Should().NotBeEmpty();
        sale.Branch.Should().NotBeNullOrWhiteSpace();
    }

    /// <summary>
    /// Tests that adding a product increases the sale total.
    /// </summary>
    [Fact(DisplayName = "Given sale When adding product Then total amount increases")]
    public void Given_Sale_When_AddingProduct_Then_TotalAmountIncreases()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var initialTotal = sale.TotalAmount;

        // Act
        sale.AddProduct("Laptop", 2, 1000m);

        // Assert
        sale.TotalAmount.Should().BeGreaterThan(initialTotal);
        sale.SaleItems.Count.Should().Be(1);
    }

    /// <summary>
    /// Tests that updating a product modifies its quantity and price.
    /// </summary>
    [Fact(DisplayName = "Given sale with product When updating product Then updates correctly")]
    public void Given_SaleWithProduct_When_UpdatingProduct_Then_UpdatesCorrectly()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.AddProduct("Smartphone", 1, 800m);

        // Act
        sale.UpdateProduct("Smartphone", 3, 750m, false);

        // Assert
        var updatedItem = sale.SaleItems.First();
        updatedItem.Quantity.Should().Be(3);
        updatedItem.UnitPrice.Should().Be(750m);
    }

    /// <summary>
    /// Tests that cancelling a sale marks all products as cancelled and resets the total amount.
    /// </summary>
    [Fact(DisplayName = "Given sale When cancelling Then all products are cancelled and total is zero")]
    public void Given_Sale_When_Cancelling_Then_AllProductsAreCancelled_And_TotalIsZero()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.AddProduct("Tablet", 2, 500m);

        // Act
        sale.Cancel();

        // Assert
        sale.IsCancelled.Should().BeTrue();
        sale.TotalAmount.Should().Be(0);
        sale.SaleItems.All(i => i.IsCancelled).Should().BeTrue();
    }

    /// <summary>
    /// Tests that an exception is thrown when trying to add more than 20 identical items.
    /// </summary>
    [Fact(DisplayName = "Given sale When adding more than 20 identical items Then throws exception")]
    public void Given_Sale_When_AddingMoreThan20Items_Then_ThrowsException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        Action act = () => sale.AddProduct("Monitor", 21, 200m);

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Cannot sell more than 20 identical items.");
    }
}
