using AutoMapper;
using E_commerce.Core.DTO;
using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using E_commerce.Core.Services;
using E_commerce.Core.Sharing;
using E_commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly IImageManagmentService _imageManagmentService;
		public ProductRepository(AppDbContext context, IMapper mapper, IImageManagmentService imageManagmentService) : base(context)
		{
			_context = context;
			_mapper = mapper;
			_imageManagmentService = imageManagmentService;
		}

		public async Task<ProductsWithCountDTO> GetAllAsync(ProductParams productParams)
		{
			var query = _context.products
				.Include(p => p.Category)
				.Include(p => p.Photos)
				.AsNoTracking();
			if (!string.IsNullOrWhiteSpace(productParams.Search))
			{
				var searchWords = productParams.Search.ToLower().Split(' ');

				query = query.Where(p =>
					searchWords.All(word =>
						p.Name.ToLower().Contains(word) ||
						p.Description.ToLower().Contains(word)
					)
				);
			}


			if (productParams.CategoryId.HasValue)
				query = query.Where(p => p.CategoryId == productParams.CategoryId);

			if (!string.IsNullOrWhiteSpace(productParams.Sort))
			{
				query = productParams.Sort switch
				{
					"PriceAce" => query.OrderBy(p => p.NewPrice),
					"PriceDesc" => query.OrderByDescending(p => p.NewPrice),
					_ => query.OrderBy(p => p.Name),
				};
			}

			ProductsWithCountDTO productsWithCountDTO = new ProductsWithCountDTO();
			productsWithCountDTO.TotalCount = query.Count();

			query = query.Skip(productParams.PageSize * (productParams.PageNumber - 1)).Take(productParams.PageSize);
			


			productsWithCountDTO.Products = _mapper.Map<List<ProductDTO>>(query);

			return productsWithCountDTO;
		}



		public async Task<bool> AddAsync(AddProductDTO productDTO)
		{
			if (productDTO == null) return false;

			var product = _mapper.Map<Product>(productDTO);
			await _context.products.AddAsync(product);
			await _context.SaveChangesAsync();
			var ImagePath = await _imageManagmentService.AddImageAsync(productDTO.Photo, productDTO.Name);
			
			var photo = ImagePath.Select(path => new Photo
			{
				ImageName = path,
				ProductId = product.Id
			}).ToList();

			await _context.Photos.AddRangeAsync(photo);
			await _context.SaveChangesAsync();
			return true;
		}


		public async Task<bool> UpdateAsync(UpdateProductDTO productDTO)
		{
			if(productDTO is null)
			{
				return false;
			}
			var FindProduct = await _context.products
				.Include(p => p.Category)
				.Include(p => p.Photos)
				.FirstOrDefaultAsync(p => p.Id == productDTO.Id);
			if (FindProduct is null)
			{
				return false;
			}
			_mapper.Map(productDTO, FindProduct);
			var FindPhoto = await _context.Photos
				.Where(ph => ph.ProductId == productDTO.Id)
				.ToListAsync();
			foreach(var item in FindPhoto)
			{
				_imageManagmentService.DeleteImageAsync(item.ImageName);
			}
			_context.RemoveRange(FindPhoto);

			var ImagePath = await _imageManagmentService.AddImageAsync(productDTO.Photo, productDTO.Name);
			var photo = ImagePath.Select(path => new Photo
			{
				ImageName = path,
				ProductId = productDTO.Id,
			}).ToList();
			await _context.Photos.AddRangeAsync(photo);
		 	await _context.SaveChangesAsync();
			return true;
			
		}

		public async Task DeleteAsync(Product product)
		{
			var photo = await _context.Photos
				.Where(ph => ph.ProductId == product.Id).ToListAsync();
			foreach(var item in photo)
			{
				_imageManagmentService.DeleteImageAsync(item.ImageName);
			}
			_context.products.Remove(product);
			await _context.SaveChangesAsync();
		}

	}
}
