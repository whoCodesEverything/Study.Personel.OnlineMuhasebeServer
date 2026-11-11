using OnlineMuhasebeServer.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany;
using OnlineMuhasebeServer.Domain.AppEntities;

//using OnlineMuhasebeServer.Application.Features.CompanyFeatures.Commands.CreateCompany;
namespace OnlineMuhasebeServer.Application.Services.AppServices
{
	public interface ICompanyService
	{
		//Task CreateCompany(CreateCompanyRequest request);

		Task CreateCompany(CreateCompanyCommand request, CancellationToken cancellationToken=default);
		Task MigrateCompanyDatabases();
		Task<Company?> GetCompanyByName(string name);

		

	}
}
