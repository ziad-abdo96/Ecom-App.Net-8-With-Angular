using Microsoft.AspNetCore.Http;

namespace E_commerce.Core.Services
{
	public interface IImageManagmentService
	{
		Task<List<string>> AddImageAsync(IFormFileCollection files, string src);
		void DeleteImageAsync(string src);
	}
}
