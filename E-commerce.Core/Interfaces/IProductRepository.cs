using E_commerce.Core.DTO;
using E_commerce.Core.Entities;
using E_commerce.Core.Sharing;

namespace E_commerce.Core.Interfaces
{
	public interface IProductRepository: IGenericRepository<Product>
	{
		Task<ProductsWithCountDTO> GetAllAsync(ProductParams productParams);
		Task<bool> AddAsync(AddProductDTO prodcutDTO);
		Task<bool> UpdateAsync(UpdateProductDTO productDTO);
		Task DeleteAsync(Product product);
	}
}
