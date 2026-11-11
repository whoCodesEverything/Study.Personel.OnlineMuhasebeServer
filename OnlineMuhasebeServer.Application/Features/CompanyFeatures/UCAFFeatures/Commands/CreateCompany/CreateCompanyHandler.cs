using MediatR;
using OnlineMuhasebeServer.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany;
using OnlineMuhasebeServer.Application.Services.AppServices;

namespace OnlineMuhasebeServer.Application.Features.CompanyFeatures.Commands.CreateCompany
{
	public sealed class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyCommandResponse>
	{
		private readonly ICompanyService _companyService;

		public CreateCompanyHandler(ICompanyService companyService)
		{
			_companyService = companyService;
		}

		//public async Task<CreateCompanyResponse> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
		//{
		//	await _companyService.CreateCompany(request, cancellationToken);

		//	return new();
		//}


		public async Task<CreateCompanyCommandResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
		{
			var appRequest = new CreateCompanyCommand(
				request.Name,
				request.ServerName,
				request.DatabaseName,
				request.UserId,
				request.Password
				);
			await _companyService.CreateCompany(appRequest, cancellationToken);
			return new CreateCompanyCommandResponse();
		}


	}
}
