using Book.Domain.Entities;
using Book.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book.Infrastructure.Contexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option): base(option)
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book.Domain.Entities.Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookIssue> BookIssues { get; set; }
        public DbSet<Department> Departments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList()) {
                switch (entry.State) { 
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasOne(s => s.Book)
                .WithMany()
                .HasForeignKey(s => s.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne<Author>()
                .WithMany()
                .HasForeignKey(s => s.AuthorId)
                .HasConstraintName("FK_BookAuthor_Author_Id");

            // seed data
            modelBuilder.Entity<Author>().HasData(
                    new Author
                    {
                        Id = 1,
                        AuthorName = "J. K. Rowling",
                        //CreatedBy = 1,
                        CreatedDate = DateTime.Now,
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
