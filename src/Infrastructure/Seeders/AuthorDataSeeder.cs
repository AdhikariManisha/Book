using Book.Domain.Entities;
using Book.Infrastructure.Contexts;
using Book.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Book.Infrastructure.Seeders;
public class AuthorDataSeeder : IDataSeeder
{
    private readonly ApplicationDbContext _context;

    public AuthorDataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SeedAsync()
    {
        var authors = new List<string>{
                                        "William Shakespare",
                                        "Charles Dickens",
                                        "Robin Hobb"
                                    };

        await AddAuthorsAsync(authors);
        await _context.SaveChangesAsync();

        return true;
    }

    private async Task AddAuthorsAsync(List<string> authors)
    {
        foreach (var authorName in authors)
        {
            var author = new Author
            {
                AuthorName = authorName,
                Status = true
            };

            var authorExist = await _context.Authors.AnyAsync(s => s.AuthorName == author.AuthorName);
            if (!authorExist)
            {
                await _context.Authors.AddAsync(author);
            }
        }
    }
}
