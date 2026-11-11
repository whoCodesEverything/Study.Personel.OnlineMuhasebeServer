using OnlineMuhasebeServer.Domain.Abstractions;

namespace OnlineMuhasebeServer.Domain.Repositories
{
	public interface ICommandRepository<T> : IRepository<T> where T : Entity
	{
		
		Task AddAsync(T Entity,CancellationToken cancellationToken);
		Task AddRangeAsync(IEnumerable<T> entities,CancellationToken cancellationToken);
		void Update(T entity);
		void UpdateRange(IEnumerable<T> entities);
		void Delete(T entity);
		Task RemoveById(Guid id);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
