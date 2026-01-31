namespace E_commerce.Core.DTO
{
	public record AddCategoryDTO (string Name, string Description);
	public record UpdateCategoryDTO (int Id, string Name, string Description);
}
