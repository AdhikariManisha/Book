﻿using AutoMapper;
using Book.Application.Contracts.Books;
using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Book.Application.Contracts.Services;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Book.Shared.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using Book.Shared.Constants;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Book.Shared.Options;

namespace Book.Application.Books;
public class BookService : IBookService
{
    private readonly IRepository<Domain.Entities.Book, int> _bookRepository;
    private readonly IRepository<BookAuthor, int> _bookAuthorRepository;
    private readonly IRepository<Author, int> _authorRepository;
    private readonly IRepository<BookGenre, int> _bookGenreRepository;
    private readonly IRepository<Genre, int> _genreRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<int> _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _cache;
    private readonly CacheOption _cacheOption;

    public BookService(
        IRepository<Domain.Entities.Book, int> bookRepository,
        IRepository<Domain.Entities.BookAuthor, int> bookAuthorRepository,
        IRepository<Domain.Entities.Author, int> authorRepository,
        IRepository<Domain.Entities.BookGenre, int> bookGenreRepository,
        IRepository<Domain.Entities.Genre, int> genreRepository,
        IMapper mapper,
        IUnitOfWork<int> unitOfWork,
        IConfiguration configuration,
        IDistributedCache cache,
        IOptions<CacheOption> cacheOption
        )
    {
        _bookRepository = bookRepository;
        _bookAuthorRepository = bookAuthorRepository;
        _authorRepository = authorRepository;
        _bookGenreRepository = bookGenreRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _cache = cache;
        _cacheOption = cacheOption.Value;
    }

    public async Task<bool> CreateAsync(CreateUpdateBookDto dto)
    {
        var bookExists = await _bookRepository.Entities.AnyAsync(s => s.BookName == dto.BookName);
        if (bookExists)
        {
            throw new Exception("Book Already exists.");
        }

        var input = _mapper.Map<Domain.Entities.Book>(dto);
        using var uow = _unitOfWork;
        var entity = await uow.Repository<Domain.Entities.Book>().CreateAsync(input);
        //await _bookRepository.CreateAsync(input);

        #region BookAuthor
        foreach (var authorId in dto.Authors)
        {
            //var authorExists = await _authorRepository.Entities.AnyAsync(s => s.Id == authorId);
            //if (authorExists)
            {
                var bookAuthor = new BookAuthor
                {
                    Book = entity,
                    AuthorId = authorId
                };
                await uow.Repository<Domain.Entities.BookAuthor>().CreateAsync(bookAuthor);
                //await _bookAuthorRepository.CreateAsync(bookAuthor);
            }
        }
        #endregion

        #region BookGenre
        foreach (var genreId in dto.Genres)
        {
            var genreExists = await _genreRepository.Entities.AnyAsync(s => s.Id == genreId);
            if (genreExists)
            {
                var bookGenre = new BookGenre
                {
                    Book = entity,
                    GenreId = genreId
                };
                await uow.Repository<Domain.Entities.BookGenre>().CreateAsync(bookGenre);
                //await _bookGenreRepository.CreateAsync(bookGenre);
            }
        }
        #endregion
        await uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var uow = _unitOfWork;
        await uow.Repository<Domain.Entities.Book>().DeleteAsync(id);

        var deleteBookAuthors = uow.Repository<Domain.Entities.BookAuthor>().Entities.Where(s => s.BookId == id);
        await uow.Repository<Domain.Entities.BookAuthor>().DeleteManyAsync(deleteBookAuthors);

        var deleteBookGenres = uow.Repository<Domain.Entities.BookGenre>().Entities.Where(s => s.BookId == id);
        await uow.Repository<Domain.Entities.BookGenre>().DeleteManyAsync(deleteBookGenres);

        await uow.CommitAsync();
        return true;
    }

    public async Task<BookDto> GetAsync(int id)
    {
        var book = await _bookRepository.GetAsync(id);
        var dto = _mapper.Map<BookDto>(book);

        #region BookAuthor
        var authors = await _authorRepository.GetListAsync();
        var bookAuthors = await _bookAuthorRepository.Entities.Where(s => s.BookId == id).ToListAsync();
        var bookAuthorDtos = (from ba in bookAuthors
                              join a in authors on ba.AuthorId equals a.Id
                              select new BookAuthorDto
                              {
                                  Id = ba.Id,
                                  AuthorId = ba.AuthorId,
                                  AuthorName = a.AuthorName
                              }).ToList();

        if (bookAuthorDtos.Count() > 0)
        {
            dto.Authors = bookAuthorDtos;
        }
        #endregion

        #region BookGenre
        var genres = await _genreRepository.GetListAsync();
        var bookGenres = await _bookGenreRepository.Entities.Where(s => s.BookId == id).ToListAsync();
        var bookGenereDtos = (from ba in bookGenres
                              join g in genres on ba.GenreId equals g.Id
                              select new BookGenreDto
                              {
                                  Id = ba.Id,
                                  GenreId = ba.GenreId,
                                  GenreName = g.GenreName
                              }).ToList();

        if (bookGenereDtos.Count() > 0)
        {
            dto.Genres = bookGenereDtos;
        }
        #endregion

        return dto;
    }

    public async Task<int> GetBookTotalCount()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var sql = "sp_GetBookTotalCount";

        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        SqlParameter outputParameter = new SqlParameter();
        outputParameter.ParameterName = "@totalCount";
        outputParameter.SqlDbType = System.Data.SqlDbType.Int;
        outputParameter.Direction = System.Data.ParameterDirection.Output;

        cmd.Parameters.Add(outputParameter);
        cmd.Parameters.AddWithValue("@Id", 2);

        await cmd.ExecuteNonQueryAsync();

        return (int)outputParameter.Value;
    }

    public async Task<List<BookDto>> GetListAsync()
    {
        //var cacheData = await _cache.GetAsync(CacheKey.Book.GetAll);
        //var dtos = JsonSerializer
        var books = await _bookRepository.GetListAsync();
        var dtos = _mapper.Map<List<BookDto>>(books);
        var dataToCacheAsString = JsonSerializer.Serialize(dtos);
        var dataToCache = Encoding.UTF8.GetBytes(dataToCacheAsString);
        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(_cacheOption.BookGetListAbsoluteExpirationInMins));
        await _cache.SetAsync(CacheKey.Book.GetAll, dataToCache, options);
        return dtos;
    }

    public async Task IssueBook(int id)
    {
        var book = await _bookRepository.GetAsync(id);
        if (book == null)
        {
            throw new ValidationException("Book Not Found");
        }

        book.IssueDate = DateTime.Now;
        book.IssueTo = 1;

        await _bookRepository.UpdateAsync(id, book);
    }

    public async Task<bool> UpdateAsync(int id, CreateUpdateBookDto dto)
    {
        var bookExists = await _bookRepository.Entities.Where(s => s.Id != id).AnyAsync(s => s.BookName == dto.BookName);
        if (bookExists)
        {
            throw new Exception("Book Already exists.");
        }

        var input = _mapper.Map<Domain.Entities.Book>(dto);

        var bookExist = await _bookRepository.Entities.Where(s => s.Id == id).FirstOrDefaultAsync();
        if (bookExist == null)
        {
            throw new Exception("Book Doesnot exists.");
        }
        bookExist.BookName = dto.BookName;

        await _bookRepository.UpdateAsync(id, bookExist);

        #region BookAuthor
        // delete existing bookauthors
        var deleteBookAuthors = _bookAuthorRepository.Entities.Where(s => s.BookId == id);
        await _bookAuthorRepository.DeleteManyAsync(deleteBookAuthors);

        foreach (var authorId in dto.Authors)
        {
            var authorExists = await _authorRepository.Entities.AnyAsync(s => s.Id == authorId);
            if (authorExists)
            {
                var bookAuthor = new BookAuthor
                {
                    BookId = id,
                    AuthorId = authorId
                };
                await _bookAuthorRepository.CreateAsync(bookAuthor);
            }
        }
        #endregion

        #region BookGenre
        // delete existing bookauthors
        var deleteBookGenres = _bookGenreRepository.Entities.Where(s => s.BookId == id);
        await _bookGenreRepository.DeleteManyAsync(deleteBookGenres);

        foreach (var genreId in dto.Genres)
        {
            var genreExists = await _genreRepository.Entities.AnyAsync(s => s.Id == genreId);
            if (genreExists)
            {
                var bookGenre = new BookGenre
                {
                    BookId = id,
                    GenreId = genreId
                };
                await _bookGenreRepository.CreateAsync(bookGenre);
            }
        }
        #endregion

        return true;
    }
}
