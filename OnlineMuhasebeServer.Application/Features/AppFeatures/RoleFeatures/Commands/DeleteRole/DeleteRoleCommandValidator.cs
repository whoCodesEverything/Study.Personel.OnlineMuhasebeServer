using FluentValidation;

namespace OnlineMuhasebeServer.Application.Features.AppFeatures.RoleFeatures.Commands.DeleteRole
{
	public sealed class DeleteRoleCommandValidator:AbstractValidator<DeleteRoleCommand>
	{
		public DeleteRoleCommandValidator()
		{
			RuleFor(p => p.Id).NotEmpty().WithMessage("id bilgisi yazılmaldır");
			RuleFor(p => p.Id).NotNull().WithMessage("id bilgisi boş olamaz");
		}
	}
}
