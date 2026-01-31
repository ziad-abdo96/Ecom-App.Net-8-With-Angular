using E_commerce.Core.Interfaces;
using E_commerce.Core.Services;
using E_commerce.Infrastructure.Data;
using E_commerce.Infrastructure.Repositories;
using E_commerce.Infrastructure.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

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
			services.AddScoped<IUnitOfWrok, UnitOfWork>();
		
			services.AddScoped<IImageManagmentService, ImageManagementService>();
			services.AddSingleton<IFileProvider>(
				new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
			services.AddDbContext<AppDbContext>(op =>
			{
				op.UseSqlServer(configuration.GetConnectionString("Ecom"));
			});
			return services;
		}
	}
}
