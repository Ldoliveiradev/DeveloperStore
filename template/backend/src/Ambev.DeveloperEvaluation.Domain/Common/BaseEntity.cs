using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; }

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }

    /// <summary>
    /// Calculates a discount based on the quantity of items purchased.
    /// </summary>
    /// <param name="quantity">The number of items purchased.</param>
    /// <param name="unitPrice">The price per unit of the product.</param>
    /// <returns>The total discount amount.</returns>
    protected static decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity >= 10 && quantity <= 20)
                return quantity * unitPrice * 0.20m;
            if (quantity >= 4)
                return quantity * unitPrice * 0.10m;

            return 0;
        }
}
