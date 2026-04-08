namespace E_commerce.Core.Sharing
{
	public class ProductParams
	{
		public string? Sort { get; set; }
		public int? CategoryId {  get; set; }
		public string? Search { get; set; }

		public int PageNumber { get; set; } = 1;
		public int MaxPageSize { get; set; } = 6;
		private int _pageSize = 10;
		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = value > MaxPageSize ? MaxPageSize : value;  }
		}

	}
}
