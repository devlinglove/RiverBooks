using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;


internal interface IBookService
{
	IEnumerator<BookDto> ListBooks();
}


public record BookDto(Guid Id, string Title, string Author);
internal class BookService : IBookService
{
	public IEnumerator<BookDto> ListBooks()
	{
		var books = new List<BookDto>
		{
			new BookDto(Guid.NewGuid(), "To Kill a Mockingbird", "Harper Lee"),
			new BookDto(Guid.NewGuid(), "1984", "George Orwell"),
			new BookDto(Guid.NewGuid(), "The Great Gatsby", "F. Scott Fitzgerald"),
			new BookDto(Guid.NewGuid(), "Moby Dick", "Herman Melville"),
			new BookDto(Guid.NewGuid(), "Pride and Prejudice", "Jane Austen")
		};

		// Return the books as an enumerator
		return books.GetEnumerator();
	}
}

public static class BookEndPoints
{
	public static void MapBookEndpoints(this WebApplication app)
	{
		app.MapGet("/books", (IBookService bookService) =>
		{
			return bookService.ListBooks();
		});
	}
}




//public class BookDto
//{
//	public BookDto(Guid id, string author, string title)
//	{
//		Id = id;
//		Author = author;
//		Title = title;
//	}

//	public Guid Id { get; set; }
//	public string Author { get; set; } = string.Empty;
//	public string Title { get; set; } = string.Empty;
//}


public static class BookServiceExtension
{
	public static IServiceCollection AddUserModule(this IServiceCollection services)
	{
		return services.AddScoped<IBookService, BookService>();
	}
}