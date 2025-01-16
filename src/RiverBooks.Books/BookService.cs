



using System.Net;
using System.Runtime.InteropServices;

namespace RiverBooks.Books;

internal class BookService : IBookService
{
  private readonly IBookRespository _bookRespository;
  public BookService(
    IBookRespository bookRespository
  )
  {
    _bookRespository = bookRespository;
  }

  public async Task AddBookAsync(BookDto bookDto)
  {
    var book = new Book(bookDto.Title, bookDto.Author, bookDto.Price);
    await _bookRespository.AddBookAsync(book);
    await _bookRespository.SaveChangesAsync();
  }

  public async Task DeleteBookAsync(Guid id)
  {
    var book = await _bookRespository.GetByIdAsync(id);
    if(book != null)
    {
      await _bookRespository.DeleteBookAsync(book);
      await _bookRespository.SaveChangesAsync();
    }
  }

  public async Task<List<BookResponseDto>> ListBooksAsync()
  {
    var books = await _bookRespository.ListBooksAsync();
    
    return books.Select((book) => new BookResponseDto
    {
      
      Id = book.Id, 
      Title = book.Title,
      Author = book.Author,
      Price = book.Price

    }).ToList();

  }

  public async Task UpdateBookPriceAsync(Guid bookId, decimal newPrice)
  {
    // validate the newPrice

    var book = await _bookRespository.GetByIdAsync(bookId);

    //TODO:  handle not found case

    book!.UpdatePrice(newPrice);
    await _bookRespository.SaveChangesAsync();
  }

  public async Task<BookResponseDto> GetBookByIdAsync(Guid id)
  {
    var book = await _bookRespository.GetByIdAsync(id);

    //TODO: Handle not found case
    var bookResponse = new BookResponseDto
    {
      Id = book!.Id,
      Title = book.Title,
      Price = book.Price,
      Author = book.Author
    };

    return bookResponse;

  }








}
