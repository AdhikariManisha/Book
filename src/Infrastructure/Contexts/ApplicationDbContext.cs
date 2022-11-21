using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option): base(option)
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookIssue> BookIssues { get; set; }

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

            modelBuilder.Entity<Author>().HasData(
                    new Author
                    {
                        Id = 1,
                        AuthorName = "J. K. Rowling",
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now,
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
