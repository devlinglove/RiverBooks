namespace RiverBooks.Books;
internal interface IBookRespository : IReadOnlyBookRespository
{
  Task AddBookAsync(Book book);
  Task DeleteBookAsync(Book book);
  Task SaveChangesAsync();
}

