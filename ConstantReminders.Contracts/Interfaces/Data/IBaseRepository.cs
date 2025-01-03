using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Contracts.Interfaces.Data;

public interface IBaseRepository<TEntity> where TEntity : class, IEntity
{
    Task<bool> ExistsAsync(Guid id);
    Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> operation);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
    Task DeleteAsync(TEntity entity);
}