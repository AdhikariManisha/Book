﻿using Book.Application.Contracts.Repositories;
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

    public async Task<bool> CreateAsync(T input)
    {
        await _applicationDbContext.Set<T>().AddAsync(input);

        return true;
    }

    public async Task<bool> DeleteAsync(TId id)
    {
        var entity = await _applicationDbContext.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new Exception($"{nameof(T)} not found.");
        }
        _applicationDbContext.Set<T>().Remove(entity);

        return true;
    }

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

    public async Task<bool> UpdateAsync(TId id, T input)
    {
        var entity = await _applicationDbContext.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new Exception($"{nameof(T)} not found.");
        }
        _applicationDbContext.Set<T>().Update(entity);

        return true;
    }
}
