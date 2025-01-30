using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Unit tests for the <see cref="SaleItem"/> entity.
/// </summary>
public class SaleItemTests
{
    /// <summary>
    /// Tests that a valid sale item is created successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale item data When creating sale item Then sale item is created successfully")]
    public void Given_ValidSaleItemData_When_CreatingSaleItem_Then_SaleItemIsCreatedSuccessfully()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();

        // Assert
        saleItem.Should().NotBeNull();
        saleItem.ProductName.Should().NotBeNullOrWhiteSpace();
        saleItem.Quantity.Should().BeGreaterThan(0);
        saleItem.UnitPrice.Should().BeGreaterThan(0);
    }

    /// <summary>
    /// Tests that the total amount is calculated correctly.
    /// </summary>
    [Fact(DisplayName = "Given sale item When calculating total Then total amount is correct")]
    public void Given_SaleItem_When_CalculatingTotal_Then_TotalAmountIsCorrect()
    {
        // Arrange
        var saleItem = new SaleItem(Guid.NewGuid(), "Headphones", 3, 100m, 20m);

        // Act
        var expectedTotal = (3 * 100m) - 20m;

        // Assert
        saleItem.TotalAmount.Should().Be(expectedTotal);
    }

    /// <summary>
    /// Tests that updating the quantity and price modifies the sale item correctly.
    /// </summary>
    [Fact(DisplayName = "Given sale item When updating quantity and price Then updates correctly")]
    public void Given_SaleItem_When_UpdatingQuantityAndPrice_Then_UpdatesCorrectly()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateSaleItemWithoutDiscount();

        // Act
        saleItem.UpdateQuantityAndPrice(5, 150m);

        // Assert
        saleItem.Quantity.Should().Be(5);
        saleItem.UnitPrice.Should().Be(150m);
    }

    /// <summary>
    /// Tests that cancelling a sale item sets all values to zero.
    /// </summary>
    [Fact(DisplayName = "Given sale item When cancelling Then all values are zero")]
    public void Given_SaleItem_When_Cancelling_Then_AllValuesAreZero()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();

        // Act
        saleItem.Cancel();

        // Assert
        saleItem.IsCancelled.Should().BeTrue();
        saleItem.TotalAmount.Should().Be(0);
        saleItem.Quantity.Should().Be(0);
        saleItem.UnitPrice.Should().Be(0);
        saleItem.Discount.Should().Be(0);
    }

    /// <summary>
    /// Tests that an exception is thrown when trying to update quantity to more than 20 items.
    /// </summary>
    [Fact(DisplayName = "Given sale item When updating quantity over 20 Then throws exception")]
    public void Given_SaleItem_When_UpdatingQuantityOver20_Then_ThrowsException()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();

        // Act
        Action act = () => saleItem.UpdateQuantityAndPrice(21, 100m);

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Cannot sell more than 20 identical items.");
    }
}
