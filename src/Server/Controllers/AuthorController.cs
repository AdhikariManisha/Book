using Book.Application.Contracts.Repositories;
using Book.Authors;
using Book.Domain.Entities;
using Book.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers;
[ApiController]
[Route("api/[Controller]")]

public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _dapperAuthorRepo;

    public AuthorController(IAuthorRepository dapperAuthorRepo)
    {
        _dapperAuthorRepo = dapperAuthorRepo;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var author = await _dapperAuthorRepo.GetAsync(id);

        return Ok(author);
    }

    /// <summary>
    ///  Get all authors
    /// </summary>
    /// <returns>Status 200 ok</returns>
    [HttpGet]
    public async Task<ActionResult> GetListAsync()
    {
        var authors = await _dapperAuthorRepo.GetListAsync();

        return Ok(authors);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateUpdateAuthorDto input)
    {
		// edited for merge conflict
        var id = await _dapperAuthorRepo.CreateAsync(input);

        return Ok(true);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _dapperAuthorRepo.DeleteAsync(id);

        return Ok(true);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, CreateUpdateAuthorDto input)
    {
        await _dapperAuthorRepo.UpdateAsync(id, input);

        return Ok(true);
    }
}