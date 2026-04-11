using E_commerce.Core.DTO;

namespace E_commerce.Core.Interfaces
{
	public interface IAuth
	{
		Task<string> RegisterAsync(RegisterDTO registerDTO);
		Task<string> LoginAsync(LoginDTO loginDTO);
		Task<bool> SendEmailForForgetPassword(string email);
		Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO);
		Task<bool> ActiveAccount(ActiveAccountDTO activeAccountDTO);
	}
}
