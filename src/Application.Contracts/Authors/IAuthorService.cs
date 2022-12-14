﻿namespace Book.Authors;
public interface IAuthorService
{
    public Task<AuthorDto> GetAsync(int id);
    public Task<AuthorDto> GetByNameAsync(string name);
    public Task<List<AuthorDto>> GetListAsync();
    public Task<bool> CreateAsync(CreateUpdateAuthorDto input);
    public Task<bool> UpdateAsync(CreateUpdateAuthorDto input);
    public Task<bool> DeleteAsync(int id );
}
