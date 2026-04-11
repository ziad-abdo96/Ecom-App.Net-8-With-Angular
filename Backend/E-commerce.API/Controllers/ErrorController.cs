using E_commerce.API.Helper;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
	[Route("errors/{statusCode}")]
	[ApiController]
	public class ErrorController : ControllerBase
	{
		[HttpGet]
		public IActionResult Error(int statusCode)
		{
			return new ObjectResult(new ResponseAPI(statusCode));
		}
	}
}
