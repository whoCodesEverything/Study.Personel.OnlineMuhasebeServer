using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Domain;
using OnlineMuhasebeServer.Domain.AppEntities;
using OnlineMuhasebeServer.Persistance.Context;

namespace OnlineMuhasebeServer.Persistance
{
	public sealed class ContextService : IContextService
	{
		private readonly AppDbContext _appContext;

		public ContextService(AppDbContext appContext)
		{
			_appContext = appContext;
		}



		/*public DbContext CreateDbContextInstance(string companyId)
		{
			//throw new NotImplementedException();
			Company company=_appContext.Set<Company>().Find(companyId);
			return new CompanyDbContext(company);

		}*/


		//public DbContext CreateDbContextInstance(string companyId)
		//{
		//	// 1. Şirketi veritabanında bulmaya çalışıyoruz
		//	Company company = _appContext.Set<Company>().Find(Guid.Parse(companyId));

		//	// 2. Eğer şirket bulunamadıysa (null döndüyse), açıkça hata verelim ki bilelim
		//	if (company == null)
		//	{
		//		throw new Exception($"ID'si '{companyId}' olan şirket sistemde bulunamadı!");
		//	}

		//	// 3. Şirket bulunduysa context'i oluşturup döndürüyoruz
		//	return new CompanyDbContext(company);
		//}


		public DbContext CreateDbContextInstance(string companyId)
		{
			if (!Guid.TryParse(companyId, out Guid parsedCompanyId))
			{
				throw new ArgumentException($"Geçersiz CompanyId formatı: {companyId}");
			}

			// Master DB'den şirketi bulmaya çalışıyoruz
			Company company = _appContext.Set<Company>().Find(parsedCompanyId);


			if (company == null)
			{
				throw new Exception($"ID'si '{companyId}' olan şirket Master veritabanında bulunamadı! Lütfen doğru bir CompanyId gönderdiğinizden emin olun.");
			}

			return new CompanyDbContext(company);
		}
	}
}
