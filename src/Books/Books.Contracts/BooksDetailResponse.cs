namespace RiverBooks.Books.Contracts
{
	public record BooksDetailResponse(Guid BookId, string Title, string Author, decimal UnitPrice);
	
}
