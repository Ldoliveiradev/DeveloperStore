using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber).IsRequired().UseIdentityAlwaysColumn();
            builder.Property(s => s.SaleDate).IsRequired();
            builder.Property(s => s.TotalAmount).IsRequired().HasPrecision(18, 2).HasDefaultValue(0);
            builder.Property(s => s.Branch).IsRequired().HasMaxLength(50);
            builder.Property(s => s.IsCancelled).IsRequired().HasDefaultValue(false);

            builder.HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.SaleItems)
               .WithOne(p => p.Sale)
               .HasForeignKey(p => p.SaleId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
