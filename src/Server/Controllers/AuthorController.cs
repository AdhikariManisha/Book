﻿using Book.Application.Contracts.Repositories;
using Book.Authors;
using Book.Domain.Entities;
using Book.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers;
[ApiController]
[Route("api/[Controller]")]

public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var author = await _authorService.GetAsync(id);

        return Ok(author);
    }

    /// <summary>
    ///  Get all authors
    /// </summary>
    /// <returns>Status 200 ok</returns>
    [HttpGet]
    public async Task<ActionResult> GetListAsync()
    {
        var authors = await _authorService.GetListAsync();

        return Ok(authors);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateUpdateAuthorDto input)
    {
        await _authorService.CreateAsync(input);

        return Ok(true);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _authorService.DeleteAsync(id);

        return Ok(true);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(CreateUpdateAuthorDto input)
    {
        await _authorService.UpdateAsync(input);

        return Ok(true);
    }
}