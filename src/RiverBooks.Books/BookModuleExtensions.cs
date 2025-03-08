using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace RiverBooks.Books;

public static class BookModuleServicesExtensions
{
	public static IServiceCollection AddBooksModuleServices(this IServiceCollection services, IConfiguration config, ILogger logger, List<Assembly> mediateRAssemblies)
	{
    string? connectionString = config.GetConnectionString("BooksConnectionString");
    services.AddDbContext<BookDbContext>(config => config.UseSqlServer(connectionString));

    services.AddScoped<IBookService, BookService>();
    services.AddScoped<IBookRespository, EfBookRepository>();
	mediateRAssemblies.Add(typeof(BookModuleServicesExtensions).Assembly);


	logger.Information("{Module} module services registered", "Books");
    return services;

		
	}
}
