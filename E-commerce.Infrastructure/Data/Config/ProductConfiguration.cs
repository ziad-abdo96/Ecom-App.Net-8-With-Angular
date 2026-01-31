using E_commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerce.Infrastructure.Data.Config
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(p => p.Name).IsRequired();
			builder.Property(p => p.Description).IsRequired();
			builder.Property(p => p.NewPrice).HasColumnType("decimal(18,2)");
			builder.HasData(
				new Product { Id = 1, Name = "test", Description = "test", CategoryId = 1, NewPrice = 12 }
				);
		}
	}
}
