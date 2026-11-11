using MediatR;
using OnlineMuhasebeServer.Application.Messaging;
using OnlineMuhasebeServer.Application.Services.AppServices;
using OnlineMuhasebeServer.Domain.AppEntities.Identity;

namespace OnlineMuhasebeServer.Application.Features.AppFeatures.RoleFeatures.Commands.DeleteRole
{
	public sealed class DeleteRoleCommandHandler:ICommandHandler<DeleteRoleCommand,DeleteRoleCommandResponse>
	{
		//private readonly RoleManager<AppRole> _roleManager;
		private readonly IRoleServices _roleServices;
		public DeleteRoleCommandHandler(IRoleServices roleServices)
		{
			_roleServices = roleServices;
		}

		//public DeleteRoleHandler(RoleManager<AppRole> roleManager)
		//{
		//	_roleManager=roleManager;
		//}

		public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommand request,CancellationToken cancellation)
		{
			AppRole role = await _roleServices.GetById(request.Id);
			if (role == null) throw new Exception("Role bulunamadı!");
			await _roleServices.DeleteAsync(role);
			return new();
		}
	}
}
