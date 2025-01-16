
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal class EfBookRepository : IBookRespository
{
  private readonly BookDbContext _dbContext;

  public EfBookRepository(BookDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public Task AddBookAsync(Book book)
  {
    _dbContext.Add(book);
    return Task.CompletedTask;
  }

  public Task DeleteBookAsync(Book book)
  {
    _dbContext.Remove(book);
    return Task.CompletedTask;
  }

  public async Task<Book?> GetByIdAsync(Guid id)
  {
    var book = await _dbContext.Books.FindAsync(id);
    return book;
  }

  public async Task<List<Book>> ListBooksAsync()
  {
    return  await _dbContext.Books.ToListAsync(); 
  }

  public async Task SaveChangesAsync()
  {
    await _dbContext.SaveChangesAsync();
  }
}
