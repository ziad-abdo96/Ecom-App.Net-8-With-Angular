using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using E_commerce.Infrastructure.Data;

namespace E_commerce.Infrastructure.Repositories
{
	public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
	{
		public PhotoRepository(AppDbContext context) : base(context)
		{
		}
	}
}
