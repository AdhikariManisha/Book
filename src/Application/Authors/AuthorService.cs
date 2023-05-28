using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Book.Authors;
public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IRepository<Author, int> _authorRepo;

    public AuthorService(IAuthorRepository authorRepository, IRepository<Author, int> authorRepo)
    {
        _authorRepository = authorRepository;
        _authorRepo = authorRepo;
    }

    public async Task<bool> CreateAsync(CreateUpdateAuthorDto input)
    {
        var authorExits = await _authorRepository.GetByNameAsync(input.AuthorName);
        if (authorExits != null)
        {
            throw new ValidationException("Author already Exits.");
        }

        await _authorRepository.CreateAsync(input);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var authorExits = await _authorRepository.GetAsync(id);
        if (authorExits == null)
        {
            throw new ValidationException("Author not found.");
        }

        await _authorRepository.DeleteAsync(id);
        return true;
    }

    public async Task<AuthorDto> GetAsync(int id)
    {
       var dto = await _authorRepository.GetAsync(id);
        return dto;
    }

    public async Task<AuthorDto> GetByNameAsync(string name)
    {
        var dto = await _authorRepository.GetByNameAsync(name);
        return dto;
    }

    public async Task<List<AuthorDto>> GetListAsync()
    {
        var dtos = await _authorRepository.GetListAsync();
        return dtos;
    }

    public async Task<bool> UpdateAsync(CreateUpdateAuthorDto input)
    {
        var authorExits = await _authorRepository.GetAsync(input.Id);
        if (authorExits == null) {
            throw new ValidationException("Author not found.");
        }

        bool isAuthorNameUpdated = authorExits.AuthorName?.ToLower() != input.AuthorName?.ToLower();
        if (isAuthorNameUpdated) // is authorname updated
        {
            var dto = await _authorRepository.GetByNameAsync(input.AuthorName);
            if (dto != null) // find author by new author name
            {
                throw new ValidationException("Author already Exits with that name.");
            }
        }

        await _authorRepository.UpdateAsync(input);
        return true;
    }
    public async Task DeleteManyAsync(List<int> ids)
    {
        if (!ids.Any())
        {
            throw new ValidationException("Empty Author List");
        }
        await _authorRepository.DeleteManyAsync(ids);
    }

    public  async Task<List<AuthorDto>> GetListByFilterAsync(AuthorFilter filter)
    {
        // var dtos = await _authorRepository.GetListByFilterAsync(filter);
        var dtos = await _authorRepo.Entities.Select(s => new AuthorDto
        {
            Id = s.Id,
            AuthorName = s.AuthorName,
            Status = s.Status,
            CreatedBy = s.CreatedBy,
            CreatedDate = s.CreatedDate,
            UpdatedBy = s.UpdatedBy,
            UpdatedDate = s.UpdatedDate
        })
        .Where(s => (string.IsNullOrWhiteSpace(filter.AuthorName) || s.AuthorName.Contains(filter.AuthorName))
            && (filter.Status == null || s.Status == filter.Status))
        .OrderByDescending(s => s.CreatedDate)
        .ToListAsync();
        return dtos;
    }
}

