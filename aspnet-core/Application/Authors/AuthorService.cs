using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Shared.Constants;
using Book.Shared.Dtos;
using Book.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Book.Authors;
public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IRepository<Author, int> _authorRepo;
    private readonly IMemoryCache _memoryCache;

    public AuthorService(IAuthorRepository authorRepository, IRepository<Author, int> authorRepo, IMemoryCache memoryCache)
    {
        _authorRepository = authorRepository;
        _authorRepo = authorRepo;
        _memoryCache = memoryCache;
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
        _memoryCache.Remove(CacheKey.Author.GetAll);
        Console.WriteLine("Remove Cache");
        return true;
    }

    public async Task<AuthorDto> GetAsync(int id)
    {
        AuthorDto dto;
        var isCache = _memoryCache.TryGetValue($"{CacheKey.Author.Get}:{id}", out dto);
        if (!isCache)
        {
            dto = await _authorRepository.GetAsync(id);
            var cacheOption = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
            };
            _memoryCache.Set($"{CacheKey.Author.Get}:{id}", dto, cacheOption);
            Console.WriteLine("Get Data from db");
        }
        else
        {
            Console.WriteLine("Get Data from cache");
        }
        return dto;
    }

    public async Task<AuthorDto> GetByNameAsync(string name)
    {
        var dto = await _authorRepository.GetByNameAsync(name);
        return dto;
    }

    public async Task<List<AuthorDto>> GetListAsync()
    {
        var dtos = (List<AuthorDto>)_memoryCache.Get(CacheKey.Author.GetAll);
        if (dtos == null)
        {
            dtos = await _authorRepository.GetListAsync();
            var cacheOption = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
            };
            cacheOption.RegisterPostEvictionCallback((key, value, reason, substate) =>
            {
                Console.WriteLine("Cache expired!");

            });
            _memoryCache.Set(CacheKey.Author.GetAll, dtos, cacheOption);
            Console.WriteLine("Get Data from db");
        }
        else
        {
            Console.WriteLine("Get Data from cache");
        }

        return dtos;
    }

    public async Task<bool> UpdateAsync(CreateUpdateAuthorDto input)
    {
        var authorExits = await _authorRepository.GetAsync(input.Id);
        if (authorExits == null)
        {
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

    public async Task<PagedResultDto<AuthorDto>> GetListByFilterAsync(PagedAndSortedResultRequestDto input, AuthorFilter filter)
    {
        // var dtos = await _authorRepository.GetListByFilterAsync(filter);

        if (string.IsNullOrWhiteSpace(input.Sorting))
        {
            input.Sorting = $"{nameof(AuthorDto.CreatedDate)} desc";
        }

        var queryable = _authorRepo.Entities.Select(s => new AuthorDto
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
        .Where(s => !filter.FromDate.HasValue || s.CreatedDate.Date >= filter.FromDate.Value.Date)
        .Where(s => !filter.ToDate.HasValue || s.CreatedDate.Date <= filter.ToDate.Value.Date)
        .OrderBy(input.Sorting);

        var totalCount = await queryable.LongCountAsync();
        var dtos = await queryable
                    .Skip(input.SkipCount)
                    .Take(input.TakeCount)
                    .ToListAsync();
        var response = new PagedResultDto<AuthorDto>(totalCount, dtos);

        return response;
    }
}

