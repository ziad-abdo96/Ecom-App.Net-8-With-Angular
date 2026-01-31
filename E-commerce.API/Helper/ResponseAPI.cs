namespace E_commerce.API.Helper
{
	public class ResponseAPI
	{
		public int StatusCode { get; set; }
		public string? Message { get; set; }
		public ResponseAPI(int statusCode, string message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetMessageFormStatusCode(statusCode);
		}

		private string GetMessageFormStatusCode(int statusCode)
		{
			return statusCode switch
			{
				200 => "Done",
				400 => "Bad Request",
				401 => "UnAuthorized",
				500 => "Server Error",
				_=>null,
			};
		}

	}
}
