using E_commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_commerce.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Product> products { get; set; }
		public virtual DbSet<Photo> Photos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
