using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany;
using OnlineMuhasebeServer.Application.Services.AppServices;
using OnlineMuhasebeServer.Domain.AppEntities;
using OnlineMuhasebeServer.Persistance.Context;

namespace OnlineMuhasebeServer.Persistance.Services.AppServices
{
	public sealed class CompanyService : ICompanyService
	{

		private static readonly Func<AppDbContext, string, Task<Company?>> GetCompanyByNameCompiled = EF.CompileAsyncQuery((AppDbContext context, string name) => context.Set<Company>().FirstOrDefault(p => p.Name == name));

		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		public CompanyService(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task CreateCompany(CreateCompanyCommand request, CancellationToken cancellationToken = default)
		{
			Company company = _mapper.Map<Company>(request);
			company.Id = Guid.NewGuid();
			await _context.Set<Company>().AddAsync(company, cancellationToken); // İsteğe bağlı olarak eklenebilir
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<Company?> GetCompanyByName(string name)
		{
			return await GetCompanyByNameCompiled(_context, name);
		}

		//public async Task MigrateCompanyDatabases()
		//{
		//	var companies = await _context.Set<Company>().ToListAsync();

		//	foreach (var company in companies)
		//	{
		//		// İsimlendirilmiş parametre kullanarak doğrudan 'company' parametresine gönderiyoruz
		//		var db = new CompanyDbContext(company: company);
		//		db.Database.Migrate();
		//	}
		//}


		public async Task MigrateCompanyDatabases()
		{
			var companies = await _context.Set<Company>().ToListAsync();

			foreach (var company in companies)
			{
				try
				{

					// Hangi şirket için işlem yapıldığını konsola yazdıralım
					Console.WriteLine($"Migrating database for company: {company.Name ?? company.Id.ToString()}");


					Console.WriteLine($"Şirket: {company.Name} - Sunucu Adı: '{company.ServerName}' - Veritabanı: '{company.DatabaseName}'");
					var db = new CompanyDbContext(company: company);

					// Eğer bağlantı dizesinin ne olduğunu görmek isterseniz:
					// Console.WriteLine($"Connection String: {db.Database.GetConnectionString()}");

					db.Database.Migrate();

					Console.WriteLine($"Successfully migrated for: {company.Name}");

					
				}
				catch (Exception ex)
				{
					// Hangi şirkette hata alındığını ve detayını net görebilmek için:
					Console.WriteLine($"ERROR: Could not connect or migrate for company {company.Name}. Details: {ex.Message}");

					
					throw;
				}
			}
		}


	}
}
