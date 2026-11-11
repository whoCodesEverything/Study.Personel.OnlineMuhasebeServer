using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Domain.Abstractions;
using OnlineMuhasebeServer.Domain.Repositories;
using OnlineMuhasebeServer.Persistance.Context;

namespace OnlineMuhasebeServer.Persistance.Repositories
{
	public class CommandRepository<T> : ICommandRepository<T> where T : Entity
	{

		private static readonly Func<CompanyDbContext, Guid, Task<T>> GetByIdCompiled =
				EF.CompileAsyncQuery((CompanyDbContext context, Guid id) => context.Set<T>().FirstOrDefault(p => p.Id == id));


		private  CompanyDbContext _context;

		public DbSet<T> Entity { get; set; }

		//DbSet<T> IRepository<T>.Entity => throw new NotImplementedException();

		public void SetDbContextInstance(DbContext context)
		{
			_context = (CompanyDbContext)context;
			Entity=_context.Set<T>();
		}
		public async Task AddAsync(T entity, CancellationToken cancellationToken)
		{
			await Entity.AddAsync(entity,cancellationToken);
		}

		public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
		{
			await Entity.AddRangeAsync(entities,cancellationToken);
		}



		public void Delete(T entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(T entity)
		{
			Entity.Remove(entity);
		}

		public async Task RemoveById(Guid id)
		{
			T entity = await GetByIdCompiled(_context, id);
			Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			Entity.RemoveRange(entities);
		}

		public void Update(T entity)
		{
			Entity.Update(entity);
		}

		public void UpdateRange(IEnumerable<T> entities)
		{
			Entity.UpdateRange(entities);
		}
	}
}
