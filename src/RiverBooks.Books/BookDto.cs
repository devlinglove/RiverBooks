namespace RiverBooks.Books;

//public record BookDto(string Title, string Author, decimal Price);

//public record BookResonseDto(Guid id, string Title, string Author, decimal Price);


public class BookDto
{
  public string Title { get; set; } = string.Empty;
  public string Author { get; set; } = string.Empty;
  public decimal Price { get; set; }
}

public class BookResponseDto : BookDto
{
  public Guid Id { get; set; }
}


