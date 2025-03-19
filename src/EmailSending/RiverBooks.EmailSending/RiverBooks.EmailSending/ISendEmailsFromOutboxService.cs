

namespace RiverBooks.EmailSending
{
	internal interface IOutboxService
	{
		Task QueEmailForSending(EmailOutboxEntity entity);
	}


}
