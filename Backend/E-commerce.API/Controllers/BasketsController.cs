using AutoMapper;
using E_commerce.API.Helper;
using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
	public class BasketsController : BaseController
	{
		public BasketsController(IUnitOfWrok work, IMapper mapper) : base(work, mapper)
		{
		}

		[HttpGet("get-basket-item/{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var result = await _work.CustomerBasketRepository.GetBasketAsync(id);
			if(result is null)
			{
				return Ok(new CustomerBasket());
			}
			return Ok(result);
		}

		[HttpPost("update-basket")]
		public async Task<IActionResult> Add(CustomerBasket basket)
		{
			var _basket = await _work.CustomerBasketRepository.UpdateBasketAsync(basket);
			return Ok(_basket);
		}

		[HttpDelete("delete-basket/{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			var result = await _work.CustomerBasketRepository.DeleteBasketAsync(id);
			return result ? Ok(new ResponseAPI(200, "item deleted")) : BadRequest(new ResponseAPI(400));
		}
	}
}
