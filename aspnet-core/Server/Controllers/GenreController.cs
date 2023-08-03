using Book.Application.Contracts.Permissions;
using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Infrastructure.Repositories;
using Book.Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = BookPermissions.Genres.Default)]
[ApiController]
[Route("/api/[Controller]")]
public class GenreController : ControllerBase
{
    private readonly IRepository<Genre, int> _genreRepository;

    public GenreController(IRepository<Genre, int> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Genre input)
    {
        var genreExists = _genreRepository.Entities.Any(s => s.GenreName == input.GenreName);
        if (genreExists)
        {
            throw new Exception("Genre Already exists.");
        }

        await _genreRepository.CreateAsync(input);

        return Ok(true);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, Genre input)
    {
        await _genreRepository.UpdateAsync(id, input);

        return Ok(true);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Roles.User))]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var genre = await _genreRepository.GetAsync(id);
        
        return Ok(genre);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> GetListAsync()
    {
        var genres = await _genreRepository.GetListAsync();

        return Ok(genres);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _genreRepository.DeleteAsync(id);

        return Ok(true);
    }
}