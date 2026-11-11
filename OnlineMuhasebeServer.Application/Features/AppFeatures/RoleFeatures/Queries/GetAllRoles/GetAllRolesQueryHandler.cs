using MediatR;
using OnlineMuhasebeServer.Application.Messaging;
using OnlineMuhasebeServer.Application.Services.AppServices;
using OnlineMuhasebeServer.Domain.AppEntities.Identity;

namespace OnlineMuhasebeServer.Application.Features.AppFeatures.RoleFeatures.Queries.GetAllRoles
{
	public sealed class GetAllRolesQueryHandler:IQueryHandler<GetAllRolesQuery,GetAllRolesQueryResponse>
	{
		private readonly IRoleServices _roleServices;

		public GetAllRolesQueryHandler(IRoleServices roleServices)
		{
			_roleServices = roleServices;
		}

		public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
		{
			IList<AppRole> roles = await _roleServices.GetAllRolesAsync();

			// Süslü parantez yerine doğrudan constructor içine parametre olarak veriyoruz:
			return new(roles);
		}
	}
}
