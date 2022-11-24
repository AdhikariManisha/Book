using Book.Application.Contracts.Repositories;
using Book.Domain.Entities;
using Book.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Book.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext context;

        public AuthorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Author> Entities => this.context.Authors;

        public async Task<bool> CreateAsync(Author input)
        {
            var authorExist = await context.Authors.AnyAsync(s => s.AuthorName == input.AuthorName);
            if (authorExist) {
                throw new InvalidOperationException("Author Already exists.");
            }

            await context.Authors.AddAsync(input);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var authorExist = await context.Authors.AnyAsync(s => s.Id == id);
            if (!authorExist)
            {
                throw new InvalidOperationException("Author does not exists.");
            }

            await context.Authors.Where(s => s.Id == id).ExecuteDeleteAsync();
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Author> GetAsync(int id)
        {
            return await context.Authors.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Author>> GetListAsync()
        {
            return await context.Authors.ToListAsync();
        }

        public async  Task<bool> UpdateAsync(int id, Author input)
        {
            var authorExist = await context.Authors.FirstOrDefaultAsync(s => s.Id == id);
            if (authorExist == null)
            {
                throw new InvalidOperationException("Author does not exists.");
            }

            authorExist.AuthorName = input.AuthorName;
            authorExist.Status = input.Status;

            context.Authors.Update(authorExist);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
