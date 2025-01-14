using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;

//public static class BookEndPoints
//{
//	public static void MapBookEndpoints(this WebApplication app)
//	{
//		app.MapGet("/books", (IBookService bookService) =>
//		{
//			return bookService.ListBooks();
//		});
//	}
//}




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