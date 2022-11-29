using AutoMapper;
using Book.Application.Contracts.BookIssues;
using Book.Application.Contracts.Books;
using Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application;
public class BookApplicationAutoMapperProfile: Profile
{
    public BookApplicationAutoMapperProfile() {
        CreateMap<CreateUpdateBookDto, Domain.Entities.Book>();
        CreateMap<Domain.Entities.Book, BookDto>();

        CreateMap<BookIssue, BookIssueDto>();
        CreateMap<CreateUpdateBookIssueDto, BookIssue>();
        CreateMap<ReturnBookIssueDto, BookIssue>();
    }
}
