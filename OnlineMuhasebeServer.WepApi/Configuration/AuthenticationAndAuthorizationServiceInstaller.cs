using Microsoft.AspNetCore.Authentication.JwtBearer;
using OnlineMuhasebeServer.WepApi.OptionsSetup;

namespace OnlineMuhasebeServer.WepApi.Configuration
{
	public class AuthenticationAndAuthorizationServiceInstaller:IServiceInstaller
	{
		public void Install(IServiceCollection services,IConfiguration configuration)
		{


			services.ConfigureOptions<JwtOptionsSetup>();
			services.ConfigureOptions<JwtBearerOptionsSetup>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
		}
	}
}
