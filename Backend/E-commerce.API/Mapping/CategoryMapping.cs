using AutoMapper;
using E_commerce.Core.DTO;
using E_commerce.Core.Entities;

namespace E_commerce.API.Mapping
{
	public class CategoryMapping: Profile
	{
		public CategoryMapping()
		{
			CreateMap<AddCategoryDTO, Category>().ReverseMap();
			CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
		}
	}
}
