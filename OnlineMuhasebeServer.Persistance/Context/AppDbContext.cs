using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineMuhasebeServer.Domain.Abstractions;
using OnlineMuhasebeServer.Domain.AppEntities;
using OnlineMuhasebeServer.Domain.AppEntities.Identity;

//uygulamanın ana master context'i

namespace OnlineMuhasebeServer.Persistance.Context
{
	public sealed class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Company> Companies { get; set; }
		public DbSet<UserAndCompanyRelationship> UserAndCompanyRelationships { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entries = ChangeTracker.Entries<Entity>();

			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property(p => p.Id).CurrentValue = Guid.NewGuid();
					entry.Property(p => p.CreateDate).CurrentValue = DateTime.Now;
				}
				if (entry.State == EntityState.Modified)
				{
					entry.Property(p => p.UpdateDate).CurrentValue = DateTime.Now;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Ignore<IdentityUserLogin<string>>();
			builder.Ignore<IdentityUserRole<string>>();
			builder.Ignore<IdentityUserClaim<string>>(); 
			builder.Ignore<IdentityUserToken<string>>();
			builder.Ignore<IdentityRoleClaim<string>>();
		}
	}

	// Factory sınıfı AppDbContext'in DIŞINA taşındı ve parametresiz constructor eklendi
	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContextFactory()
		{
		}

		public AppDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder();

			// Bağlantı cümlenizi buraya eklemelisiniz
			var connectionString = "Data Source=DESKTOP-VBDD1T0\\SQLEXPRESS;Initial Catalog=MuhasebeMasterDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
			optionsBuilder.UseSqlServer(connectionString);

			return new AppDbContext(optionsBuilder.Options);
		}
	}
}