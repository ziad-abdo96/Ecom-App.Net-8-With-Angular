using AutoMapper;
using E_commerce.Core.Interfaces;
using E_commerce.Core.Services;
using E_commerce.Infrastructure.Data;

namespace E_commerce.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWrok
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly  IImageManagmentService _imageManagmentService;

		public ICategoryRepository CategoryRepository { get; }
		public IProductRepository ProductRepository { get; }
		public IPhotoRepository PhotoRepository { get; }

		public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagmentService imageManagmentService)
		{
			_context = context;
			_mapper = mapper;
			_imageManagmentService = imageManagmentService;

			CategoryRepository = new CategoryRepository(_context);
			ProductRepository = new ProductRepository(_context, _mapper, _imageManagmentService);
			PhotoRepository = new PhotoRepository(_context);
		}
	}

}
