namespace RiverBooks.EmailSending
{
	internal interface ISenderEmail
	{
		Task SendEmailAsync(string to, string from, string subject, string body);
	}


}
