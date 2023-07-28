using Book.Domain.Entities;
using Book.Infrastructure.Contexts;
using Book.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Book.Infrastructure.Seeders;
public class GenreDataSeeder : IDataSeeder
{
    private readonly ApplicationDbContext _context;

    public GenreDataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SeedAsync()
    {
        var genres = new List<string>{
                                        "Drama",
                                        "Fantasy",
                                        "Adventure",
                                        "Romance",
                                        "Action"
                                    };

        await AddGenreAsync(genres);
        await _context.SaveChangesAsync();

        return true;
    }

    private async Task AddGenreAsync(List<string> genres)
    {
        foreach (var genreName in genres)
        {
            var genre = new Genre
            {
                GenreName = genreName,
                Status = true
            };

            var genreExist = await _context.Genres.AnyAsync(s => s.GenreName == genre.GenreName);
            if (!genreExist)
            {
                await _context.Genres.AddAsync(genre);
            }
        }
    }
}
