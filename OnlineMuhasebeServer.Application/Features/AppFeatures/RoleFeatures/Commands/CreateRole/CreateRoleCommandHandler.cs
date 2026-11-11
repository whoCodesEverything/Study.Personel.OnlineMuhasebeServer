using OnlineMuhasebeServer.Application.Messaging;
using OnlineMuhasebeServer.Application.Services.AppServices;
using OnlineMuhasebeServer.Domain.AppEntities.Identity;

namespace OnlineMuhasebeServer.Application.Features.AppFeatures.RoleFeatures.Commands.CreateRole
{
	public sealed class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, CreateRoleCommandResponse>
	{
		private readonly IRoleServices _roleServices;
		public CreateRoleCommandHandler(IRoleServices roleServices)
		{
			_roleServices=roleServices;
		}

		public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
		{
			AppRole role = await _roleServices.GetByCode(request.Code);
			if (role != null) throw new Exception("bu rol daha önce kayıt edilmiştir");
			role = new AppRole
			{
				Id = Guid.NewGuid().ToString(),
				Code = request.Code,
				Name = request.Name
			};
			await _roleServices.AddAsync(request);
			return new();
		}
	}
}
