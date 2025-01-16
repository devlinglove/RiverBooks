using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.Books;

public static class BookModuleExtensions
{
	public static IServiceCollection AddBooksModule(this IServiceCollection services, IConfiguration config, ILogger logger)
	{
    string? connectionString = config.GetConnectionString("BooksConnectionString");
    services.AddDbContext<BookDbContext>(config => config.UseSqlServer(connectionString));

    services.AddScoped<IBookService, BookService>();
    services.AddScoped<IBookRespository, EfBookRepository>();
    //services.AddControllers().AddApplicationPart(typeof(BooksController).Assembly);

    logger.Information("{Module} module services registered", "Books");
    return services;

		
	}
}
