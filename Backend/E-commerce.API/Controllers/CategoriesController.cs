using AutoMapper;
using E_commerce.API.Helper;
using E_commerce.Core.DTO;
using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : BaseController
	{
		public CategoriesController(IUnitOfWrok work, IMapper mapper) : base(work, mapper)
		{
		}

		[HttpGet("get-all")]
		public async Task<IActionResult> Get()
		{
			try
			{
				var category = await _work.CategoryRepository.GetAllAsync();
				if(category == null)
				{
					return BadRequest(new ResponseAPI(400));
				}

				return Ok(category);
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
				var category = await _work.CategoryRepository.GetByIdAsync(id);
				if (category == null)
					return BadRequest(new ResponseAPI(400, $"Not Found Category Id={id}"));
				return Ok(category);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);	
			}
		}

		[HttpPost("add-category")]
		public async Task<IActionResult> Add(AddCategoryDTO categoryDTO)
		{
			try
			{
				var category = _mapper.Map<Category>(categoryDTO);
				 await _work.CategoryRepository.AddAsync(category);
				return Ok(new ResponseAPI(200, "Item has been added"));
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("update-category")]
		public async Task<IActionResult> Update(UpdateCategoryDTO categoryDTO)
		{
			try
			{
				var category = _mapper.Map<Category>(categoryDTO);
				await _work.CategoryRepository.UpdateAsync(category);
				return Ok(new ResponseAPI(200, "Item has been updated"));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("delete-category/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _work.CategoryRepository.DeleteAsync(id);
				return Ok(new ResponseAPI(200, "Item has been deleted"));
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
	}
}
