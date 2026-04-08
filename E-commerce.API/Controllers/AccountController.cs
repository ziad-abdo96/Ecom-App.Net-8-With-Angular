using AutoMapper;
using E_commerce.API.Helper;
using E_commerce.Core.DTO;
using E_commerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
	public class AccountController : BaseController
	{
		public AccountController(IUnitOfWrok work, IMapper mapper) : base(work, mapper)
		{
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterDTO registerDTO)
		{
			var result = await _work.AuthRepository.RegisterAsync(registerDTO);
			if (result != "done")
			{
				return BadRequest(new ResponseAPI(400, result));
			}

			return Ok(new ResponseAPI(200, result));
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginDTO loginDTO)
		{
			var result = await _work.AuthRepository.LoginAsync(loginDTO);
			if (result.StartsWith("Please"))
			{
				return BadRequest(new ResponseAPI(400, result));
			}
			Response.Cookies.Append("token", result, new CookieOptions
			{
				Secure = true,
				HttpOnly = true,
				Domain = "localhost",
				Expires = DateTime.Now.AddDays(1),
				IsEssential = true,
				SameSite = SameSiteMode.Strict,
			});
			return Ok(new ResponseAPI(200));

		}

		[HttpPost("active-account")]
		public async Task<IActionResult> Acctive(ActiveAccountDTO activeAccountDTO)
		{
			var result = await _work.AuthRepository.ActiveAccount(activeAccountDTO);
			return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(200));
		}

		[HttpGet("send-email-forget-password")]
		public async Task<IActionResult> Forget(string email)
		{
			var result = await _work.AuthRepository.SendEmailForForgetPassword(email);
			return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(200));
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
		{
			var result = await _work.AuthRepository.ResetPassword(resetPasswordDTO);
			if (result == "Password change success")
			{
				return Ok(new ResponseAPI(200));
			}
			return BadRequest(new ResponseAPI(200));
		}
	}
}
