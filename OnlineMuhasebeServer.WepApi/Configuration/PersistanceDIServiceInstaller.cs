
using OnlineMuhasebeServer.Application.Services.AppServices;
using OnlineMuhasebeServer.Application.Services.CompanyServices;
using OnlineMuhasebeServer.Domain;
using OnlineMuhasebeServer.Domain.Repositories.UCAFRepositories;
using OnlineMuhasebeServer.Persistance;
using OnlineMuhasebeServer.Persistance.Repositories.UCAFRepositories;
using OnlineMuhasebeServer.Persistance.Services.AppServices;
using OnlineMuhasebeServer.Persistance.Services.CompanyServices;

namespace OnlineMuhasebeServer.WepApi.Configuration
{
	public class PersistanceDIServiceInstaller : IServiceInstaller
	{
		public void Install(IServiceCollection services, IConfiguration configuration)
		{
			//throw new NotImplementedException();
			#region Context UnitOfWork
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IContextService, ContextService>();

			#endregion

			#region Services
			services.AddScoped<ICompanyService, CompanyService>();
			services.AddScoped<IUCAFService,UCAFService>();
			services.AddScoped<IRoleServices,RoleService>();

			#endregion

			#region Repositories
			services.AddScoped<IUCAFQueryRepository, UCAFQueryRepository>();
			services.AddScoped<IUCAFService, UCAFService>();

			#endregion
		}
	}
}
