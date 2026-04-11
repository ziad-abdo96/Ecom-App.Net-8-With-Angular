
using AutoMapper;
using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using E_commerce.Core.Services;
using E_commerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

namespace E_commerce.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWrok
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly  IImageManagmentService _imageManagmentService;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IEmailService _emailService;
		private readonly IGenerateToken _generateToken;

		public ICategoryRepository CategoryRepository { get; }
		public IProductRepository ProductRepository { get; }
		public IPhotoRepository PhotoRepository { get; }

		public ICustomerBasketRepository CustomerBasketRepository { get; }

		public IAuth AuthRepository { get; }

		public UnitOfWork(AppDbContext context, IMapper mapper,
						IImageManagmentService imageManagmentService,
						IConnectionMultiplexer redis, UserManager<AppUser> userManager,
						SignInManager<AppUser> signInManager,
						IEmailService emailService, IGenerateToken generateToken)
		{
			_context = context;
			_mapper = mapper;
			_imageManagmentService = imageManagmentService;
			_userManager = userManager;
			_signInManager = signInManager;
			_emailService = emailService;
			_generateToken = generateToken;


			CategoryRepository = new CategoryRepository(_context);
			ProductRepository = new ProductRepository(_context, _mapper, _imageManagmentService);
			PhotoRepository = new PhotoRepository(_context);
			CustomerBasketRepository = new CustomerBasketRepository(redis);
			AuthRepository = new AuthRepository(_userManager, _emailService, _signInManager, _generateToken);
		}
	}

}
