using AutoMapper;
using Book.Application.Contracts.Books;
using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book.Server.Controllers;

[ApiController]
[Route("/api/[Controller]")]
//[Authorize()]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(
        IBookService bookService
    )
    {
        _bookService = bookService;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(int id)
    {
        var dto = await _bookService.GetAsync(id);

        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult> GetListAsync()
    {
        var books = await _bookService.GetListAsync();

        return Ok(books);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateUpdateBookDto dto)
    {
        bool taskCompleted = await _bookService.CreateAsync(dto);

        return Ok(taskCompleted);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, CreateUpdateBookDto dto)
    {
        bool taskCompleted = await _bookService.UpdateAsync(id, dto);

        return Ok(taskCompleted);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var taskCompleted = await _bookService.DeleteAsync(id);

        return Ok(taskCompleted);
    }
    [HttpGet("GetBookTotalCount")]
    public async Task<ActionResult> GetBookTotalCount()
    {
        var books = await _bookService.GetBookTotalCount();

        return Ok(books);
    }
}
