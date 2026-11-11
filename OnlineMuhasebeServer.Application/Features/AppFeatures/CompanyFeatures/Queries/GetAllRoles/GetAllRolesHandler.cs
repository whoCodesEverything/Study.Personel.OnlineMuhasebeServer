using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Domain.AppEntities.Identity;

namespace OnlineMuhasebeServer.Application.Features.AppFeatures.CompanyFeatures.Queries.GetAllRoles
{
	public sealed class GetAllRolesHandler : IRequestHandler<GetAllRolesRequest, GetAllRolesResponse>
	{
		private readonly RoleManager<AppRole> _roleManager;

		public GetAllRolesHandler(RoleManager<AppRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<GetAllRolesResponse> Handle(GetAllRolesRequest request, CancellationToken cancellationToken)
		{

			IList<AppRole> roles = await _roleManager.Roles.ToListAsync();
			return new GetAllRolesResponse { Roles=roles };
		}
	}
}
