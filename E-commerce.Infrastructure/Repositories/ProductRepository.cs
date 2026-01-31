using E_commerce.Core.DTO;
using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using AutoMapper;
using E_commerce.Infrastructure.Data;
using E_commerce.Core.Services;
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
	}
}
