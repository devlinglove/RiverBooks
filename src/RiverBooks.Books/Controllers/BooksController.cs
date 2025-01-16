using Microsoft.AspNetCore.Mvc;

namespace RiverBooks.Books.Controllers;


[ApiController]
[Route("api/[controller]")]

public class BooksController : ControllerBase
{
  private readonly IBookService _bookService;
  public BooksController(IBookService bookService)
  {
    _bookService = bookService;
  }

  [HttpGet]
  public async Task<IActionResult> GetBooksAsync()
  {
    var books = await _bookService.ListBooksAsync();
    return Ok(books);
  }
}


