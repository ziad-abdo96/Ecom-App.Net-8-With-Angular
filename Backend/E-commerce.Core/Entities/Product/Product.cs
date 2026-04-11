namespace E_commerce.Core.Entities
{
	public class Product : BaseEntity<int>
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal NewPrice { get; set; }
		public decimal OldPrice { get; set; }
		public virtual List<Photo> Photos { get; set; } = new List<Photo>();
		public int CategoryId { get; set; }	
		public virtual Category Category { get; set; }
	}
}
