using OnlineMuhasebeServer.Application.Messaging;
using OnlineMuhasebeServer.Application.Services.AppServices;
using OnlineMuhasebeServer.Domain.AppEntities.Identity;

namespace OnlineMuhasebeServer.Application.Features.AppFeatures.RoleFeatures.Commands.UpdateRole
{
	public sealed class UpdateRoleCommandHandler:ICommandHandler<UpdateRoleCommand, UpdateRoleCommandResponse>
	{
		private readonly IRoleServices _roleServices;

		public UpdateRoleCommandHandler(IRoleServices roleServices)
		{
			_roleServices = roleServices;
		}

		public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommand request, CancellationToken cancellation) {
			AppRole role = await _roleServices.GetById(request.Id);
			if (role == null) throw new Exception("Role bulunamadı!");
			if (role.Code != request.Code)
			{
				AppRole checkcode=await _roleServices.GetByCode(request.Code);
				if (checkcode != null) throw new Exception("Bu kod daha önce kaydedilmiş");

			}
			role.Code = request.Code;
			role.Name = request.Name;
			await _roleServices.UpdateAsync(role);
			return new();
		}
	}
	
}
