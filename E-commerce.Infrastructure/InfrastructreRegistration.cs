using E_commerce.Core.Interfaces;
using E_commerce.Infrastructure.Data;
using E_commerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
			services.AddDbContext<AppDbContext>(op =>
			{
				op.UseSqlServer(configuration.GetConnectionString("Ecom"));
			});
			return services;
		}
	}
}
