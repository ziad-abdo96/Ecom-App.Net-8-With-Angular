using AutoMapper;
using E_commerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected readonly IUnitOfWrok _work;
		protected readonly IMapper _mapper;

		public BaseController(IUnitOfWrok work, IMapper mapper)
		{
			this._work = work;
			_mapper = mapper;
		}
	}
}
