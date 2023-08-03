using Book.Application.Contracts.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Book.Application.Contracts.Repositories;

namespace Book.Authors;
public class AuthorRepository : IAuthorRepository
{
    private readonly IDbConnection _dbConnection;

    public AuthorRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> CreateAsync(CreateUpdateAuthorDto input)
    {
        var conn = _dbConnection.GetConnection();
        var sql = "Insert into Authors(AuthorName, Status, CreatedDate) values(@AuthorName, @Status, GetDate());" +
            " select SCOPE_IDENTITY();";
        var id = await conn.ExecuteScalarAsync<int>(sql, new { input.AuthorName, input.Status });
        return id;
    }

    public async Task DeleteAsync(int id)
    {
        var conn = _dbConnection.GetConnection();
        var sql = "Delete from Authors Where Id = @id";
        _ = await conn.ExecuteAsync(sql, new { Id = id });
    }

    public async Task DeleteManyAsync(List<int> ids)
    {
        var conn = _dbConnection.GetConnection();
        var sql = "Delete from Authors where Id in @ids";
        _ = await conn.ExecuteAsync(sql, new {ids = ids });
    }

    public async Task<AuthorDto> GetAsync(int id)
    {
        var conn = _dbConnection.GetConnection();
        var sql = "select * from Authors where Id = @Id";
        var author = await conn.QueryAsync<AuthorDto>(sql, new {Id = id });
        return author.FirstOrDefault();
    }

    public async Task<AuthorDto> GetByNameAsync(string name)
    {
        var conn = _dbConnection.GetConnection();
        var sql = "select * from Authors where AuthorName = @AuthorName";
        var author = await conn.QueryAsync<AuthorDto>(sql, new { AuthorName = name});
        return author.FirstOrDefault();
    }

    public async Task<List<AuthorDto>> GetListAsync()
    {
        var conn = _dbConnection.GetConnection();
        var sql = "select * from Authors";
        var authors = await conn.QueryAsync<AuthorDto>(sql);
        return authors.ToList();        
    }

    public async Task<List<AuthorDto>> GetListByFilterAsync(AuthorFilter filter)
    {
        var conn = _dbConnection.GetConnection();
        var sql = "select * from Authors as A where (@AuthorName is null or A.AuthorName like @AuthorName + '%')" +
            " and (@Status is null or A.Status=@Status)";
        var authors = await conn.QueryAsync<AuthorDto>(sql, new { filter.AuthorName, filter.Status });
        return authors.ToList();
    }

    public async Task UpdateAsync(CreateUpdateAuthorDto input)
    {
        var conn = _dbConnection.GetConnection();
        var sql = "Update Authors set AuthorName=@AuthorName, Status=@Status, UpdatedDate=GetDate() where Id = @Id";
        _ = await conn.ExecuteAsync(sql, input);        
    }
}
