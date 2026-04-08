using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using E_commerce.Core.Services;
using E_commerce.Infrastructure.Data;
using E_commerce.Infrastructure.Repositories;
using E_commerce.Infrastructure.Repositories.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace E_commerce.Infrastructure
{
	public static class InfrastructreRegistration
	{
		public static IServiceCollection infrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			//services.AddTransient
			//services.AddScoped
			//services.AddSingleton
			
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			//apply Unit of work
			services.AddScoped<IUnitOfWrok, UnitOfWork>();

			//register email sender
			services.AddScoped<IEmailService, EmailService>();
		
			//register token
			services.AddScoped<IGenerateToken,  GenerateToken>();
			
			//register image
			services.AddScoped<IImageManagmentService, ImageManagementService>();
			services.AddSingleton<IFileProvider>(
				new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
			services.AddDbContext<AppDbContext>(op =>
			{
				op.UseSqlServer(configuration.GetConnectionString("Ecom"));
			});


			//apply Redis connecction
			services.AddSingleton<IConnectionMultiplexer>(i =>
			{
				var config = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
				return ConnectionMultiplexer.Connect(config);
			});






			services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



			services.AddAuthentication(op =>
			{
				op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				op.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			}).AddCookie(op =>
			{
				op.Cookie.Name = "token";
				op.Events.OnRedirectToLogin = context =>
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					return Task.CompletedTask;
				};
			}).AddJwtBearer(op =>
			{
				op.RequireHttpsMetadata = false;
				op.SaveToken = true;
				op.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"])),
					ValidateIssuer = true,
					ValidIssuer = configuration["Token:Issuer"],
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero,
				};
				op.Events = new JwtBearerEvents()
				{
					OnMessageReceived = context =>
					{
						context.Token = context.Request.Cookies["token"];
						return Task.CompletedTask;
					}
				};
			});
		  
			return services;
		}
	}
}
