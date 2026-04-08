using E_commerce.Core.Entities;

namespace E_commerce.Core.Services
{
	public interface IGenerateToken
	{
		string GetAndCreateToken(AppUser user);
	}
}
