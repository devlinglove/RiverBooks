using System;
using System.Linq.Expressions;
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

		var dates = new List<DateTime> {
	new DateTime(2025, 2, 15),
	new DateTime(2025, 3, 20),
	new DateTime(2025, 2, 25)
};

		
		// Create the expression
		var expression = BuildDateTimeContainsExpression("8");

		// Compile the expression to a delegate
		var predicate = expression.Compile();

		// Use with LINQ Where
		var filteredDates = dates.Where(predicate).ToList();

		Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy")}{DateTime.Now.ToString("dd/MM/yyyy").Contains("02")}");

		var books = await _bookService.ListBooksAsync();
    return Ok(books);
  }




// Create a function that builds the expression for you
public static Expression<Func<DateTime, bool>> BuildDateTimeContainsExpression(string searchText)
{
	// Parameter for the DateTime input
	var parameter = Expression.Parameter(typeof(DateTime), "date");

		// Get the ToString method of DateTime
		var toStringMethod = typeof(DateTime).GetMethod("ToString", new[] { typeof(string) });

		// Create a constant for the date format
		var formatConstant = Expression.Constant("dd/MM/yyyy");

		// Call ToString() on the DateTime parameter
		var toStringCall = Expression.Call(parameter, toStringMethod, formatConstant);

		// Get the Contains method from string
		var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

	// Create a constant for the search text
	var searchConstant = Expression.Constant(searchText);

	// Call Contains(searchText) on the string result
	var containsCall = Expression.Call(toStringCall, containsMethod, searchConstant);

	// Build the lambda expression: date => date.ToString().Contains(searchText)
	return Expression.Lambda<Func<DateTime, bool>>(containsCall, parameter);
}









}


