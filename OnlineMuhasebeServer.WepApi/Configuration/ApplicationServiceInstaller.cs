using FluentValidation;
using MediatR;
using OnlineMuhasebeServer.Application.Behavior;
using System.Reflection.Metadata;

namespace OnlineMuhasebeServer.WepApi.Configuration
{
	public class ApplicationServiceInstaller : IServiceInstaller
	{
		//public void Install(IServiceCollection services, IConfiguration configuration)
		//{
		//	services.AddMediatR(cfg =>
		//	{
		//		cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
		//	});
		//}

		public void Install(IServiceCollection services, IConfiguration configuration)
		{
			// Assembly yerine direkt tip vererek kaydediyoruz:
			//services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUCAFCommand>());

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
			services.AddTransient(typeof(IPipelineBehavior<,>), (typeof(ValidationBehavior<,>)));
			services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

		}
	}
	
 }
