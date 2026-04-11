using AutoMapper;
using E_commerce.API.Helper;
using E_commerce.Core.DTO;
using E_commerce.Core.Interfaces;
using E_commerce.Core.Sharing;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{

	public class ProductsController : BaseController
	{
		public ProductsController(IUnitOfWrok work, IMapper mapper) : base(work, mapper)
		{
		}

		[HttpGet("get-all")]
		public async Task<IActionResult> Get([FromQuery] ProductParams productParams)
		{
			try
			{
				var products = await _work.ProductRepository
					.GetAllAsync(productParams);
				
				return Ok(new Pagination<ProductDTO>(productParams.PageNumber, productParams.MaxPageSize, products.TotalCount,  products.Products));
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpGet("get-by-id/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var product = await _work.ProductRepository.GetByIdAsync(id, x=>x.Category, x=>x.Photos);
				var result = _mapper.Map<ProductDTO>(product);
				if (product is null) return BadRequest(new ResponseAPI(400, "Item Not Found"));

				return Ok(result);
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
		[HttpPost("add-product")]
		public async Task<IActionResult> Add(AddProductDTO productDTO)
		{
			try
			{
				await _work.ProductRepository.AddAsync(productDTO);
				return Created();

			}
			catch (Exception ex)
			{

				return BadRequest(new ResponseAPI(400, ex.Message));
			}
		}

		[HttpPut("update-product")]
		public async Task<IActionResult> Update(UpdateProductDTO productDTO)
		{
			try
			{
				await _work.ProductRepository.UpdateAsync(productDTO);
				return Ok(new ResponseAPI(200, "product is updated succefully"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ResponseAPI(400, ex.Message));
			}
		}

		[HttpDelete("delete-product/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var product = await _work.ProductRepository
					.GetByIdAsync(id, p => p.Photos, p => p.Category);
				await _work.ProductRepository.DeleteAsync(product);
				return Ok(new ResponseAPI(200, "product is deleted succefully"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ResponseAPI(400, ex.Message));
			}
		}

	}
}
