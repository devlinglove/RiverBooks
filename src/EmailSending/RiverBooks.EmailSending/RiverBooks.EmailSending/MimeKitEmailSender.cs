using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;



namespace RiverBooks.EmailSending
{
	internal class MimeKitEmailSender : ISenderEmail
	{
		private readonly ILogger _logger;

		public MimeKitEmailSender(ILogger logger)
		{
			_logger = logger;
		}
		public async Task SendEmailAsync(string to, string from, string subject, string body)
		{
			_logger.LogInformation("Attempting to send email to {to} from {from} with subject {subject}...", to, from, subject);


			using (var client = new SmtpClient())
			{
				client.Connect("smtp.freesmtpservers.com", 25, false);
				var message = new MimeMessage();
				message.To.Add(new MailboxAddress(to, to));
				message.To.Add(new MailboxAddress(from, from));
				message.Subject = subject;
				message.Body = new TextPart("plain") { Text = body };
				await client.SendAsync(message);

				_logger.LogInformation("Email sent!");
				client.Disconnect(true);
			}

			
		}
	}


}
