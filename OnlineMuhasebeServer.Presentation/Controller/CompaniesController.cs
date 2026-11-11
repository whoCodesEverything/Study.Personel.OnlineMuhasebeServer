using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineMuhasebeServer.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany;
using OnlineMuhasebeServer.Application.Features.AppFeatures.CompanyFeatures.Commands.MigrateCompanyDatabase;
using OnlineMuhasebeServer.Presentation.Abstraction;

namespace OnlineMuhasebeServer.Presentation.Controller
{
	[Route("api/[controller]")]
	public class CompaniesController : ApiController
	{
		private readonly IMediator _mediator;
		public CompaniesController(IMediator mediator) : base(mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> CreateCompany(CreateCompanyCommand request,CancellationToken cancellationToken)
		{
			CreateCompanyCommandResponse response = await _mediator.Send(request,cancellationToken);
			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> MigrateCompanyDatabases()
		{
			MigrateCompanyDatabasesCommand request = new();
			MigrateCompanyDatabasesCommandResponse response = await _mediator.Send(request);

			return Ok(response);
		}
		
	}
}
