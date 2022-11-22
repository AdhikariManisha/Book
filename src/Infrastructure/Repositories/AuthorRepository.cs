using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Infrastructure.Contexts;

namespace Book.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext context;

        public AuthorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateAsync(Author input)
        {
            await context.AddAsync(input);
            context.SaveChanges();
            return true;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Author>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, Author input)
        {
            throw new NotImplementedException();
        }
    }
}
