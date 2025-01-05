using ConstantReminders.Contracts.Interfaces.Data;
using ConstantReminders.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstantReminders.Data;

public class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await context.Set<TEntity>().AnyAsync(p => p.Id == id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> operation)
    {
        var strategy = context.Database.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var result = await operation();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> List()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }
}