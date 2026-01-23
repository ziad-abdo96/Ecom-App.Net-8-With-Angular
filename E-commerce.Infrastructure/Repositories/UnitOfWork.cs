using E_commerce.Core.Interfaces;
using E_commerce.Infrastructure.Data;

namespace E_commerce.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWrok
	{
		private readonly AppDbContext _context;

		public ICategoryRepository CategoryRepository { get; }
		public IProductRepository ProductRepository { get; }
		public IPhotoRepository PhotoRepository { get; }

		public UnitOfWork(AppDbContext context)
		{
			_context = context;

			CategoryRepository = new CategoryRepository(_context);
			ProductRepository = new ProductRepository(_context);
			PhotoRepository = new PhotoRepository(_context);
		}
	}

}
