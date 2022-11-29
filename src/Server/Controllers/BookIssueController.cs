using AutoMapper;
using Book.Application.Contracts.BookIssues;
using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Server.Services;

namespace Book.Server.Controllers;

public class BookIssueController : CrudService<BookIssue, int, CreateUpdateBookIssueDto, BookIssueDto>
{
    public BookIssueController(IRepository<BookIssue, int> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
