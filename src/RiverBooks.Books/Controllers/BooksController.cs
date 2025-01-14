using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using RiverBooks.Books;


namespace RiverBooks.Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(
            IBookService bookService
           )
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _bookService.ListBooks();
            return Ok(books);
        }


    }
}


//namespace RiverBooks.Books.Controllers
//{
//    public static class BookController
//    {
//        public static void MapBookEndpoints(this WebApplication app)
//        {
//            app.MapGet("/books", (IBookService bookService) =>
//            {
//                return bookService.ListBooks();
//            });
//        }
//    }
//}