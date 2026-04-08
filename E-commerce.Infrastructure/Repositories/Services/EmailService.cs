
using E_commerce.Core.DTO;
using E_commerce.Core.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace E_commerce.Infrastructure.Repositories.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;
		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task SendEmail(EmailDTO emailDTO)
		{
			//prepare the message
			MimeMessage message = new();
			message.From.Add(new MailboxAddress("My Ecom",_configuration["EmailSetting:From"]));
			message.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));
			message.Subject = emailDTO.Subject;
			message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = emailDTO.Content
			};

			//create connetion with host
			using(var smtp = new MailKit.Net.Smtp.SmtpClient())
			{
				try
				{
					await smtp.ConnectAsync(
						_configuration["EmailSetting:smtp"],
						int.Parse(_configuration["EmailSetting:Port"]), false);

					await smtp.AuthenticateAsync(_configuration["EmailSetting:UserName"], _configuration["EmailSetting:Password"]);
					await smtp.SendAsync(message);
				}
				catch(Exception ex)
				{
					throw;
				}
				finally
				{
					smtp.Disconnect(true);
					smtp.Dispose();
				}
			}
		}
	}
}
