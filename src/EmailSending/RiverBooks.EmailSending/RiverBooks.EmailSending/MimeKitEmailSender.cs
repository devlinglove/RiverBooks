using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;



namespace RiverBooks.EmailSending
{
	internal class MimeKitEmailSender : ISenderEmail
	{
		private readonly ILogger<MimeKitEmailSender> _logger;

		public MimeKitEmailSender(ILogger<MimeKitEmailSender> logger)
		{
			_logger = logger;
		}
		public async Task SendEmailAsync(string to, string from, string subject, string body)
		{
			_logger.LogInformation("Attempting to send email to {to} from {from} with subject {subject}...", to, from, subject);


			using (var client = new SmtpClient())
			{
				try
				{
					client.Connect("localhost", 25, false);
					var message = new MimeMessage();
					message.From.Add(new MailboxAddress(from, from));
					message.To.Add(new MailboxAddress(to, to));
					message.Subject = subject;
					message.Body = new TextPart("plain") { Text = body };
					await client.SendAsync(message);

					_logger.LogInformation("Email sent!");
					client.Disconnect(true);

					_logger.LogInformation("Email sent!");
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "An error occurred while sending the email.");
				}



			}

			
		}
	}


}
