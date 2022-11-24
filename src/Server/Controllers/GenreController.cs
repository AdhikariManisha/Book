using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers;

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

        bool taskCompleted = await _genreRepository.CreateAsync(input);

        return Ok(taskCompleted);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, Genre input)
    {
      var taskCompleted = await _genreRepository.UpdateAsync(id, input);

        return Ok(taskCompleted);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var genre = await _genreRepository.GetAsync(id);
        
        return Ok(genre);
    }

    [HttpGet]
    public async Task<ActionResult> GetListAsync()
    {
        var genres = await _genreRepository.GetListAsync();

        return Ok(genres);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var taskCompleted = await _genreRepository.DeleteAsync(id);

        return Ok(taskCompleted);
    }
}