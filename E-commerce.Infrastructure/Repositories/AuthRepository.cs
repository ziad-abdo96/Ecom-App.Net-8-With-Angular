using E_commerce.Core.DTO;
using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using E_commerce.Core.Services;
using E_commerce.Core.Sharing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;

namespace E_commerce.Infrastructure.Repositories
{
	public  class AuthRepository : IAuth
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IEmailService _emailService;
		private readonly IGenerateToken _generateToken;
		public AuthRepository(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken generateToken)
		{
			_userManager = userManager;
			_emailService = emailService;
			_signInManager = signInManager;
			_generateToken = generateToken;
		}

		public async Task<string> RegisterAsync(RegisterDTO registerDTO)
		{
			if(registerDTO == null)
			{
				return null;
			}
			if(await _userManager.FindByNameAsync(registerDTO.UserName) is not null)
			{
				return "this username is already registered";
			}
			if(await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
			{
				return "this email is already registered";
			}

			var user = new AppUser()
			{
				UserName = registerDTO.UserName,
				Email = registerDTO.Email,
				DisplayName = registerDTO.DisplayName,
			};

			var result = await _userManager.CreateAsync(user, registerDTO.Password);
			if(result.Succeeded is not true)
			{
				return result.Errors.ToList()[0].Description;
			}
			//send active email
			string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			await SendEmail(user.Email, token, "active", "ActiveEmail", "please active you email, click to active");
			//email string body
			return "done";
		}

		public async Task SendEmail(string email, string code, string component, string subject, string message)
		{
			var result = new EmailDTO(email, "ziadabdh0100@gmail.com"
				, subject, EmailStringBody.Send(email, code, component, message));
			await _emailService.SendEmail(result);

		}

		public async Task<string> LoginAsync(LoginDTO loginDTO)
		{
			if (loginDTO == null)
				return null;

			var findUser = await _userManager.FindByEmailAsync(loginDTO.Email);
			if (!findUser.EmailConfirmed)
			{
				string token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);
				await SendEmail(findUser.Email, token, "active", "ActiveEmail", "please active you email, click to active");
				return "Please confirem you email first, we have send activat to your E-mail";
			}
			
			var result = await _signInManager.CheckPasswordSignInAsync(findUser, loginDTO.Password, true);
			
			if (result.Succeeded)
			{
				return _generateToken.GetAndCreateToken(findUser);
			}

			return "Please check your email and password, something went wrong";

		}

		public async Task<bool> SendEmailForForgetPassword(string email)
		{
			var findUser = await _userManager.FindByEmailAsync(email);
			if(findUser is null)
			{
				return false;
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(findUser);
			var encodedToken = WebUtility.UrlEncode(token);
			await SendEmail(findUser.Email, encodedToken, "Reset-Password", "ActiveEmail", "reset password click here");
			return true;
		}

		public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO)
		{
			var findUser = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
			if(findUser is null)
			{
				return null;
			}

			var decodedToken = WebUtility.UrlDecode(resetPasswordDTO.Token);

			var result = await _userManager.ResetPasswordAsync(findUser, decodedToken, resetPasswordDTO.Password);

			if(result.Succeeded)
			{
				return "Password change success";	
			}
			return result.Errors.ToList()[0].Description;
		}
		
		
		public async Task<bool> ActiveAccount(ActiveAccountDTO activeAccountDTO)
		{
			var findUser = await _userManager.FindByEmailAsync(activeAccountDTO.Email);
			if(findUser is null)
			{
				return false;
			}

			var result = await _userManager.ConfirmEmailAsync(findUser, activeAccountDTO.Token);
			
			if(result.Succeeded)
			{
				return true;
			}

			var token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);
			var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

			await SendEmail(findUser.Email, encodedToken, "active", "ActiveEmail", "please active you email, click to active");
			return false;
		}
	}
}