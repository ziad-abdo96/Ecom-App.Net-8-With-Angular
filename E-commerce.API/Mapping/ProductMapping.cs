using AutoMapper;
using E_commerce.Core.DTO;
using E_commerce.Core.Entities;

namespace E_commerce.API.Mapping
{
	public class ProductMapping: Profile
	{
		public ProductMapping()
		{
			CreateMap<Product, ProductDTO>()
				.ForMember(x => x.CategoryName, op =>
				{
					op.MapFrom(x => x.Category.Name);
				})
				.ReverseMap();

			CreateMap<Photo, PhotoDTO>().ReverseMap();
			CreateMap<AddProductDTO, Product>()
				.ForMember(p => p.Photos, op =>
				{
					op.Ignore();
				})
				.ReverseMap();
			CreateMap<UpdateCategoryDTO, Product>()
				.ForMember(p => p.Photos, op =>
				{
					op.Ignore();
				})
				.ReverseMap();
		}
	}
}
