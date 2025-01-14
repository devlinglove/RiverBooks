

namespace RiverBooks.Books;

public class BookService : IBookService
{
    public List<BookDto> ListBooks()
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

        return books.ToList();
    }

}
