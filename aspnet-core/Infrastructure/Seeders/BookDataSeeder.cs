using Book.Infrastructure.Contexts;
using Book.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.Seeders;
public class BookDataSeeder : IDataSeeder
{
    private readonly ApplicationDbContext _context;

    public BookDataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SeedAsync()
    {
        var entity = new Domain.Entities.Book { 
            BookName = "The Alchemist",
        };
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return true;
    }
}
