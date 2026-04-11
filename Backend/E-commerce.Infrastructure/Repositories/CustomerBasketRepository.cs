using E_commerce.Core.Entities;
using E_commerce.Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace E_commerce.Infrastructure.Repositories
{
	public class CustomerBasketRepository : ICustomerBasketRepository
	{
		private readonly IDatabase _database;
		
		public CustomerBasketRepository(IConnectionMultiplexer redis)
		{
			_database = redis.GetDatabase();
		}

		public async Task<CustomerBasket> GetBasketAsync(string id)
		{
			var result = await _database.StringGetAsync(id);
			if(!string.IsNullOrEmpty(result))
			{
				return JsonSerializer.Deserialize<CustomerBasket>(result);
			}
			return null;
		}


		public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
		{
			var _basket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(3));
			if(_basket)
			{
				return await GetBasketAsync(basket.Id);
			}
			return basket;
		}

		public async Task<bool> DeleteBasketAsync(string id)
		{
			return await _database.KeyDeleteAsync(id);
		}
	}
}
