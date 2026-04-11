namespace E_commerce.API.Helper
{
	public class ApiExceptions : ResponseAPI
	{
		public string Details { get; set; }
		public ApiExceptions(int statusCode, string message = null, string details = null) : base(statusCode, message)
		{
			Details = details;
		}

	}
}
