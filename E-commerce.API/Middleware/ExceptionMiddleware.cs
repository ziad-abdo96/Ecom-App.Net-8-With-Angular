using E_commerce.API.Helper;
using System.Net;
using System.Text.Json;

namespace E_commerce.API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IWebHostEnvironment _environment;
		public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment environment)
		{
			_next = next;
			_environment = environment;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";
				var response = _environment.IsDevelopment() ?
					new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
					: new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message);
				var json = JsonSerializer.Serialize(response);
				await context.Response.WriteAsync(json);
			}
		}
	}
}
