namespace RiverBooks.Books;

public interface IBookService
{
	Task<List<BookResponseDto>> ListBooksAsync();

  Task<BookResponseDto> GetBookByIdAsync(Guid id);

  Task AddBookAsync(BookDto bookDto);

  Task UpdateBookPriceAsync(Guid id, decimal newPrice);
  Task DeleteBookAsync(Guid id);


}
