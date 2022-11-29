using AutoMapper;
using Book.Application.Contracts.BookIssues;
using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers;

public class BookIssueController : CrudService<BookIssue, int, CreateUpdateBookIssueDto, BookIssueDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<User, int> _userRepository;
    private readonly IRepository<Domain.Entities.Book, int> _bookRepository;

    public BookIssueController(
        IRepository<BookIssue, int> repository, 
        IMapper mapper,
        IRepository<User, int> userRepository,
        IRepository<Domain.Entities.Book, int> bookRepository
    ) : base(repository, mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }

    public override async Task<ActionResult> GetListAsync()
    {
        var _bookIssues = await Repository.GetListAsync();
        var _users = await _userRepository.GetListAsync();
        var _books = await _bookRepository.GetListAsync();

        var bookIssues = (from bi in _bookIssues
                          join b in _books on bi.BookId equals b.Id
                          join u in _users on bi.IssueTo equals u.Id
                          join ur in _users on bi.ReturnBy equals ur.Id into urj
                          from lj1 in urj.DefaultIfEmpty()
                          select new BookIssueDto
                          {
                              Id = bi.Id,
                              BookId = bi.BookId,
                              BookName = b.BookName,
                              IssueTo = u.Id,
                              IssueToName = $"{u.Name} {u.Surname}",
                              IssueDate = bi.IssueDate,
                              ReturnDate = bi.ReturnDate,
                              ReturnBy = bi.ReturnBy,
                              ReturnByName = $"{lj1?.Name} {lj1?.Surname}"
                          }).ToList();

        return Ok(bookIssues);
    }

    public override async Task<ActionResult> CreateAsync(CreateUpdateBookIssueDto dto)
    {
        var book = await _bookRepository.GetAsync(dto.BookId);
        if (book != null) {
            book.IssueTo = dto.IssueTo;
            book.IssueDate = dto.IssueDate;
            book.IsInLibrary = false;

            await _bookRepository.UpdateAsync(dto.BookId, book);
        }

        return await base.CreateAsync(dto);
    }

    [HttpPut("return/{id}")]
    public async Task<ActionResult> ReturnBookAsync(int id, ReturnBookIssueDto dto)
    {
        var book = await _bookRepository.GetAsync(dto.BookId);
        if (book != null)
        {
            book.ReturnBy = dto.ReturnBy;
            book.ReturnDate = dto.ReturnDate;
            book.IsInLibrary = true;
            book.BookIssueId = id;

            await _bookRepository.UpdateAsync(dto.BookId, book);
        }

        var entity = await Repository.GetAsync(id);
        _mapper.Map(dto, entity);
        var taskCompleted = await Repository.UpdateAsync(id, entity);

        return Ok(taskCompleted);
    }
}
