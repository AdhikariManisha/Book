using Book.Application.Contracts.Repositories;
using Book.Authors;
using Book.Domain.Entities;
using Book.Shared;
using Book.Shared.Dtos;
using Book.Shared.Exceptions;
using Microsoft.AspNetCore.Components.Forms;
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
        try
        {
            await _authorService.CreateAsync(input);

            return Ok(true);
        }
        catch (Exception ex)
        {
            if (ex is ValidationException)
            {
                return BadRequest(new { Message = ex.Message });
            }
            else
            {
                return BadRequest(new { Message = "internal server error" });
            }
        }
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

    [HttpDelete]
    public async Task<ActionResult> DeleteManyAsync(List<int> ids)
    {
        await _authorService.DeleteManyAsync(ids);
        return Ok(true);
    }
    [HttpGet("get-list-by-filter")]
    public async Task<ActionResult> GetListByFilterAsync([FromQuery]PagedResultRequestDto input, [FromQuery]AuthorFilter filter)
    {
        var authors = await _authorService.GetListByFilterAsync(input, filter);
        var response = new ResponseModel<PagedResultDto<AuthorDto>>(true, authors);
        return Ok(response);
    }

}