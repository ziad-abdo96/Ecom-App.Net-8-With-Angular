using E_commerce.Core.Entities;

namespace E_commerce.Core.Interfaces
{
	public interface ICustomerBasketRepository
	{
		Task<CustomerBasket> GetBasketAsync(string id);
		Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
		Task<bool> DeleteBasketAsync(string id);

	}
}
