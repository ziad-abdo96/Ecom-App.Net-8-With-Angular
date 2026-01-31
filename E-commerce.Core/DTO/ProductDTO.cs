using Microsoft.AspNetCore.Http;

namespace E_commerce.Core.DTO
{
	public record ProductDTO
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public virtual List<PhotoDTO> Photos { get; set; }
		public string CategoryName { get; set; }
	}
	public record PhotoDTO
	{
		public string ImageName { get; set; }
		public int ProductId { get; set; }
	}

	public record AddProductDTO
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal NewPrice { get; set; }
		public decimal OldPrice { get; set; }
		public int CategoryId { get; set; }	
		public IFormFileCollection Photo { get; set; }

	}

	public record UpdateProductDTO : AddProductDTO
	{
		public int Id { get; set; }
	}
}
