using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.Repositories;
public class Repository<T, TId> : IRepository<T, TId> where T : BaseEntity<TId>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public IQueryable<T> Entities => _applicationDbContext.Set<T>();

    public async Task<T> GetAsync(TId id)
    {
        var entity = await _applicationDbContext.Set<T>().FindAsync(id);

        return entity;
    }

    public async Task<List<T>> GetListAsync()
    {
        var entities = await _applicationDbContext.Set<T>().ToListAsync();

        return entities;
    }

    public async Task<T> CreateAsync(T input)
    {
        await _applicationDbContext.Set<T>().AddAsync(input);
        await _applicationDbContext.SaveChangesAsync();

        return input;
    }

    public async  Task CreateManyAsync(IEnumerable<T> entities)
    {
        await _applicationDbContext.Set<T>().AddRangeAsync(entities);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TId id, T input)
    {
        var entity = await _applicationDbContext.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new Exception($"{typeof(T).Name} not found.");
        }
        _applicationDbContext.Set<T>().Update(input);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task UpdateManyAsync(IEnumerable<T> entities)
    {
        _applicationDbContext.Set<T>().UpdateRange(entities);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TId id)
    {
        var entity = await _applicationDbContext.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new Exception($"{typeof(T).Name} not found.");
        }
        _applicationDbContext.Set<T>().Remove(entity);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task DeleteManyAsync(IEnumerable<T> entities)
    {
        _applicationDbContext.Set<T>().RemoveRange(entities);
        await _applicationDbContext.SaveChangesAsync();
    }
}
