﻿using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers;
[ApiController]
[Route("api/[Controller]")]

public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;

    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var author = await _authorRepository.GetAsync(id);

        return Ok(author);
    }

    /// <summary>
    ///  Get all authors
    /// </summary>
    /// <returns>Status 200 ok</returns>
    [HttpGet]
    public async Task<ActionResult> GetListAsync()
    {
        var authors = await _authorRepository.GetListAsync();

        return Ok(authors);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Author input)
    {
        var taskCompleted = await _authorRepository.CreateAsync(input);

        return Ok(taskCompleted);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var taskCompleted = await _authorRepository.DeleteAsync(id);

        return Ok(taskCompleted);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, Author input)
    {
        var taskCompleted = await _authorRepository.UpdateAsync(id, input);

        return Ok(taskCompleted);
    }
}