using MediatR;
using RiverBooks.Books.Contracts;
using Ardalis.Result;
using System.Net;

namespace RiverBooks.Books.Integrations
{
	internal class BooksDetailQueryHandler : IRequestHandler<BooksDetailQuery, Result<BooksDetailResponse>>
	{
		private readonly IBookRespository _bookRespository;

		public BooksDetailQueryHandler(IBookRespository bookRespository)
		{
			_bookRespository = bookRespository;
		}
		public async Task<Result<BooksDetailResponse>> Handle(BooksDetailQuery request, CancellationToken cancellationToken)
		{
			var book = await _bookRespository.GetByIdAsync(request.id);
			if(book == null)
			{
				return Result.NotFound();
			}
			var response = new BooksDetailResponse(book.Id, book.Title, book.Author, book.Price);
			return response;
		}
	}
}
