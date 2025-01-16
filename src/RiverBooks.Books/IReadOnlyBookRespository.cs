namespace RiverBooks.Books;

internal interface IReadOnlyBookRespository
{
  Task<Book?> GetByIdAsync(Guid id);
  Task<List<Book>> ListBooksAsync();
}

