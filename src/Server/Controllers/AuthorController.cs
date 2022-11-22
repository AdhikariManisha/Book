using Book.Application.Contracts.Repositories;
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
    public async Task<ActionResult> GetAsync(int id) { 
        var author = await _authorRepository.GetAsync(id);
        return Ok(author);
    }
    [HttpGet]
    public async Task<ActionResult> GetListAsync() {
        var authors = await _authorRepository.GetListAsync();
        return Ok(authors);
    }
}
