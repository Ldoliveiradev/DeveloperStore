using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.ProductName)
             .IsRequired()
             .HasMaxLength(100);

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.Property(p => p.UnitPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.Discount)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasDefaultValue(0);

            builder.Property(p => p.TotalAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.IsCancelled).IsRequired().HasDefaultValue(false);

            builder.HasOne(s => s.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(p => p.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
