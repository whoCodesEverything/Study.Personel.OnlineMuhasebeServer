
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using OnlineMuhasebeServer.WepApi.Middleware;

namespace OnlineMuhasebeServer.WepApi.Configuration
{
	public class PresentationServiceInstaller : IServiceInstaller
	{
		public void Install(IServiceCollection services, IConfiguration configuration)
		{

			services.AddScoped<ExceptionMiddleware>();
			services.AddControllers().AddApplicationPart(typeof(OnlineMuhasebeServer.Persistance.AssemblyReference).Assembly);
			services.AddControllers().AddApplicationPart(typeof(OnlineMuhasebeServer.Presentation.AssemblyReference).Assembly);
			//builder.Services.AddAutoMapper(typeof(OnlineMuhasebeServer.Persistance.AssemblyReference).Assembly);


			// Veya doğrudan tip / katman belirterek:

			// Application katmanındaki herhangi bir profile referans vererek kayıt yapın:

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(setup =>
			{
				var jwtSecuritySheme = new OpenApiSecurityScheme
				{
					BearerFormat = "JWT",
					Name = "JWT Authentication",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = JwtBearerDefaults.AuthenticationScheme,
					Description = "Put *** _ONLY_** yourt JWT Bearer token on textbox below!",
					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}

				};
				setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);
				setup.AddSecurityRequirement(new OpenApiSecurityRequirement
					{
						{jwtSecuritySheme,Array.Empty<string>() }

					});
			});
		}
	}
}
