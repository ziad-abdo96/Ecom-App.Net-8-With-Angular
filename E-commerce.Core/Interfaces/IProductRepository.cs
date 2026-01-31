using E_commerce.Core.DTO;
using E_commerce.Core.Entities;

namespace E_commerce.Core.Interfaces
{
	public interface IProductRepository: IGenericRepository<Product>
	{
		Task<bool> AddAsync(AddProductDTO prodcutDTO);
		Task<bool> UpdateAsync(UpdateProductDTO productDTO);
	}
}
