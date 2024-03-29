﻿using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers;

[ApiController]
[Route("/api/[Controller]")]
public class ApplicationService<T, TId> : ControllerBase { 
    protected readonly IRepository<T, TId> Repository;

    public ApplicationService(IRepository<T, TId> repository)
    {
        Repository = repository;
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult> GetAsync(TId id)
    {
        var genre = await Repository.GetAsync(id);

        return Ok(genre);
    }

    [HttpGet]
    public virtual async Task<ActionResult> GetListAsync()
    {
        var genres = await Repository.GetListAsync();

        return Ok(genres);
    }

    [HttpPost]
    public virtual async Task<ActionResult> CreateAsync(T input)
    {
        await Repository.CreateAsync(input);

        return Ok(true);
    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult> UpdateAsync(TId id, T input)
    {
        await Repository.UpdateAsync(id, input);

        return Ok(true);
    }

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult> DeleteAsync(TId id)
    {
        await Repository.DeleteAsync(id);

        return Ok(true);
    }
}