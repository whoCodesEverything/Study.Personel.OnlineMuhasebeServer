
using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Domain.AppEntities.Identity;
using OnlineMuhasebeServer.Persistance.Context;
using OnlineMuhasebeServer.Persistance.Mapping;

namespace OnlineMuhasebeServer.WepApi.Configuration
{
	public class PersistanceServiceInstaller : IServiceInstaller
	{
		private readonly string SectionName = "SqlServer";
		public void Install(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString(SectionName)));

			services.AddIdentity<AppUser, AppRole>()
				.AddEntityFrameworkStores<AppDbContext>();

			services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);


		}
	}
}