using E_commerce.Core.DTO;

namespace E_commerce.Core.Services
{
	public interface IEmailService
	{
		Task SendEmail(EmailDTO emailDTO);
	}
}
