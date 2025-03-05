
using Ardalis.Result;
using MediatR;

namespace RiverBooks.Books.Contracts
{
	public record BooksDetailQuery(Guid id): IRequest<Result<BooksDetailResponse>>;
	
}

