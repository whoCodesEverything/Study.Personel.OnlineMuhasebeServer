/*using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineMuhasebeServer.Domain.AppEntities;

namespace OnlineMuhasebeServer.Persistance.Context
{

	public sealed class CompanyDbContext : DbContext
	{
		private readonly string _connectionString;

		// EF Core ve dışarıdan kullanım için constructor
		public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
		{
		}

		private readonly Company _company;

		// Yapıcı metodun Company almasını sağlıyoruz
		public CompanyDbContext(Company company)
		{
			_company = company;
		}

		// 3. Ana constructor (Tüm bağlantı dizesi mantığı burada)
		public CompanyDbContext(AppDbContext appDbContext = null, Company company = null)
		{
			_company = company;

			if (company != null)
			{
				// Şirket bilgilerinin eksik olup olmadığını kontrol edelim
				if (string.IsNullOrEmpty(company.ServerName) || string.IsNullOrEmpty(company.DatabaseName))
				{
					throw new ArgumentException($"Hata: '{company.Name}' adlı şirketin ServerName veya DatabaseName bilgisi veritabanında boş/null kaydedilmiş!");
				}

				// UserId null, boş veya Swagger'dan kalan "string" kelimesiyse Windows Authentication kullan
				bool isWindowsAuth = string.IsNullOrEmpty(company.UserId) || company.UserId.Equals("string", StringComparison.OrdinalIgnoreCase);

				if (isWindowsAuth)
				{
					_connectionString = $"Data Source={company.ServerName};" +
									   $"Initial Catalog={company.DatabaseName};" +
									   $"Integrated Security=True;" +
									   $"Connect Timeout=30;" +
									   $"Encrypt=False;" +
									   $"TrustServerCertificate=True;" +
									   $"ApplicationIntent=ReadWrite;" +
									   $"MultiSubnetFailover=False;";
				}
				else
				{
					// Gerçek SQL Server Authentication (Kullanıcı adı ve şifre ile)
					_connectionString = $"Data Source={company.ServerName};" +
									   $"Initial Catalog={company.DatabaseName};" +
									   $"User Id={company.UserId};" +
									   $"Password={company.Password};" +
									   $"Integrated Security=False;" +
									   $"Connect Timeout=30;" +
									   $"Encrypt=False;" +
									   $"TrustServerCertificate=True;" +
									   $"ApplicationIntent=ReadWrite;" +
									   $"MultiSubnetFailover=False;";
				}
			}
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				if (string.IsNullOrEmpty(_connectionString))
				{
					throw new InvalidOperationException("Connection string oluşturulamadı! 'Company' nesnesi null geldi veya eksik parametre içeriyor.");
				}

				optionsBuilder.UseSqlServer(_connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) =>
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

		public class CompanyDbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
		{
			public CompanyDbContext CreateDbContext(string[] args)
			{
				var optionsBuilder = new DbContextOptionsBuilder<CompanyDbContext>();
				optionsBuilder.UseSqlServer("Server=localhost;Database=MasterDb;Trusted_Connection=True;TrustServerCertificate=True;");

				return new CompanyDbContext(optionsBuilder.Options);
			}
		}
	}	
}

*/


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineMuhasebeServer.Domain.AppEntities;

namespace OnlineMuhasebeServer.Persistance.Context
{
	public sealed class CompanyDbContext : DbContext
	{
		private readonly string _connectionString;
		private readonly Company _company;

		public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
		{
		}

		public CompanyDbContext(Company company) : this(null, company)
		{
		}

		public CompanyDbContext(AppDbContext appDbContext = null, Company company = null)
		{
			_company = company;

			if (company != null && !string.IsNullOrEmpty(company.ServerName) && !string.IsNullOrEmpty(company.DatabaseName))
			{
				bool isWindowsAuth = string.IsNullOrEmpty(company.ServerUserId) || company.ServerUserId.Equals("string", StringComparison.OrdinalIgnoreCase);

				if (isWindowsAuth)
				{
					_connectionString = $"Data Source={company.ServerName};" +
									   $"Initial Catalog={company.DatabaseName};" +
									   $"Integrated Security=True;" +
									   $"Connect Timeout=30;" +
									   $"Encrypt=False;" +
									   $"TrustServerCertificate=True;" +
									   $"ApplicationIntent=ReadWrite;" +
									   $"MultiSubnetFailover=False;";
				}
				else
				{
					_connectionString = $"Data Source={company.ServerName};" +
									   $"Initial Catalog={company.DatabaseName};" +
									   $"User Id={company.ServerUserId};" +
									   $"Password={company.ServerPassword};" +
									   $"Integrated Security=False;" +
									   $"Connect Timeout=30;" +
									   $"Encrypt=False;" +
									   $"TrustServerCertificate=True;" +
									   $"ApplicationIntent=ReadWrite;" +
									   $"MultiSubnetFailover=False;";
				}
			}
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				// Eğer geçerli bir connection string oluştuysa onu kullan
				if (!string.IsNullOrEmpty(_connectionString))
				{
					optionsBuilder.UseSqlServer(_connectionString);
				}
				else
				{
					// Şirket bulunamadıysa veya parametreler boşsa, uygulamanın anında çökmesini 
					// engellemek için master/varsayılan bir veritabanına yönlendiriyoruz. 
					// (Böylece sistem hata fırlatmak yerine isteği işleyebilir ya da debug edebilirsiniz)
					optionsBuilder.UseSqlServer("Server=localhost;Database=OnlineMuhasebeMasterDb;Trusted_Connection=True;TrustServerCertificate=True;");
				}
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) =>
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

		public class CompanyDbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
		{
			public CompanyDbContext CreateDbContext(string[] args)
			{
				var optionsBuilder = new DbContextOptionsBuilder<CompanyDbContext>();
				optionsBuilder.UseSqlServer("Server=localhost;Database=MasterDb;Trusted_Connection=True;TrustServerCertificate=True;");

				return new CompanyDbContext(optionsBuilder.Options);
			}
		}
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entries = ChangeTracker.Entries<OnlineMuhasebeServer.Domain.Abstractions.Entity>();

			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property(p => p.CreateDate).CurrentValue = DateTime.Now;
				}
				if (entry.State == EntityState.Modified)
				{
					entry.Property(p => p.UpdateDate).CurrentValue = DateTime.Now;
				}
			} 
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}