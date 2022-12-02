using Book.Application.Contracts.Repositories;
using Book.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Services;
public interface IUnitOfWork<TId> : IDisposable
{
    IRepository<T, TId> Repository<T>() where T : BaseEntity<TId>;
    Task<int> CommitAsync();
    Task RollbackAsync();

}
