using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Books.Controllers;

namespace RiverBooks.Books;

public static class BookServiceExtension
{
	public static IServiceCollection AddBooksModule(this IServiceCollection services, IConfiguration config)
	{
    string? connectionString = config.GetConnectionString("BooksConnectionString");
    services.AddDbContext<BookDbContext>(config => config.UseSqlServer(connectionString));

    services.AddScoped<IBookService, BookService>();
    services.AddScoped<IBookRespository, EfBookRepository>();
    //services.AddControllers().AddApplicationPart(typeof(BooksController).Assembly);

    return services;

		
	}
}
