using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.Services;
using Book.Domain.Entities;
using Book.Infrastructure.Contexts;
using Book.Infrastructure.Repositories;
using Book.Shared.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.Services;
public class UnitOfWork<TId> : IUnitOfWork<TId>
{
    private readonly ApplicationDbContext _dbContext;
    private bool disposed = false;
    private Hashtable _repositories = new Hashtable();
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed) {
            if (disposing) { 
                _dbContext.Dispose();
            }
        }
    }

    public IRepository<T, TId> Repository<T>() where T : BaseEntity<TId>
    {
        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type)) {
            var repositoryType = typeof(Repository<,>);
            var repositoyInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _dbContext, false);
            _repositories.Add(type, repositoyInstance);
        }
        return (IRepository<T, TId>)_repositories[type];
    }

    public async Task RollbackAsync()
    {
        var entries = _dbContext.ChangeTracker.Entries().ToList();
        foreach (var entry in entries) {
            await entry.ReloadAsync();
        }
    }
}
