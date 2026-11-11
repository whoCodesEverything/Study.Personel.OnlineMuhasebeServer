using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Domain;
using OnlineMuhasebeServer.Persistance.Context;

namespace OnlineMuhasebeServer.Persistance
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private CompanyDbContext _context;
		public void SetDbContextInstance(DbContext context)
		{
			_context = (CompanyDbContext)context;
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			//throw new NotImplementedException();
			int count = await _context.SaveChangesAsync(cancellationToken);

			return count;
		}
	}

}
