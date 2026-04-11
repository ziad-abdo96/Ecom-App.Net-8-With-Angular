using E_commerce.Core.Entities;
using E_commerce.Core.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commerce.Infrastructure.Repositories.Services
{
	public class GenerateToken : IGenerateToken
	{
		private readonly IConfiguration _configuration;
		public GenerateToken(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string GetAndCreateToken(AppUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email)
			};

			var key = new SymmetricSecurityKey(
	Encoding.UTF8.GetBytes(_configuration["Token:Secret"])
);

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var tokenDescriptor = new SecurityTokenDescriptor 
			{
				Issuer = _configuration["Token:Issuer"],
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};
			var handler = new JwtSecurityTokenHandler();
			var token = handler.CreateToken(tokenDescriptor);
			return handler.WriteToken(token);
		}
	}
}
