using E_commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerce.Infrastructure.Data.Config
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.Property(c => c.Name).IsRequired().HasMaxLength(30);
			builder.Property(c => c.Id).IsRequired();

			builder.HasData(
				new Category { Id = 1, Name = "test", Description = "test" }
				);
		}
	}
}
