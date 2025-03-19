using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases.User.GetById
{
	internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
	{
		private readonly IApplicationUserRepository _applicationUserRepository;

		public GetUserByIdQueryHandler(IApplicationUserRepository applicationUserRepository)
		{
			_applicationUserRepository = applicationUserRepository;
		}
		public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			var appUser = await _applicationUserRepository.GetUserByIdAsync(request.UserId);
			if (appUser == null) {
				return Result.NotFound();
			}

			return new UserDTO(Guid.Parse(appUser.Id), appUser.Email);
		
		}
	}



}


