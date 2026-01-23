using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using E_commerce.Infrastructure.Data;

namespace E_commerce.Infrastructure.Repositories
{
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(AppDbContext context) : base(context)
		{
		}
	}
}
