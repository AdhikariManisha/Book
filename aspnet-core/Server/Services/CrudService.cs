using AutoMapper;
using Book.Application.Contracts.Books;
using Book.Application.Contracts.Repositories;
using Book.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Book.Server.Services;
[ApiController]
[Route("/api/[Controller]")]
public class CrudService<T, TId, TCreateUpdateDto, TDto> : ControllerBase
{
    protected readonly IRepository<T, TId> Repository;
    private readonly IMapper _mapper;

    public CrudService(
        IRepository<T, TId> repository,
        IMapper mapper
    ){
        Repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult> GetAsync(TId id)
    {
        var entity = await Repository.GetAsync(id);
        var dto = _mapper.Map<TDto>(entity);

        return Ok(dto);
    }

    [HttpGet]
    public virtual async Task<ActionResult> GetListAsync()
    {
        var entities = await Repository.GetListAsync();
        var dtos = _mapper.Map<List<TDto>>(entities);

        return Ok(dtos);
    }

    [HttpPost]
    public virtual async Task<ActionResult> CreateAsync(TCreateUpdateDto dto)
    {
        var input = _mapper.Map<T>(dto);
        await Repository.CreateAsync(input);
        return Ok(true);
    }
    [HttpPut("{id}")]

    public virtual async Task<ActionResult> UpdateAsync(TId id, TCreateUpdateDto dto)
    {
        var entity = await Repository.GetAsync(id);
        _mapper.Map(dto, entity);
        await Repository.UpdateAsync(id, entity);

        return Ok(true);
    }

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult> DeleteAsync(TId id)
    {
        await Repository.DeleteAsync(id);
        return Ok(true);
    }
}
