namespace E_commerce.Core.Entities
{
	public class Photo : BaseEntity<int>
	{
		public string ImageName { get; set; } = string.Empty;
		public int ProductId { get; set; }
		//public virtual Product Product { get; set; } = new Product();
	}
}
