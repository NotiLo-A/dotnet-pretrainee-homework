using HW4.Models;
using Microsoft.EntityFrameworkCore;

namespace HW4.Data;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "J. K. Rowling", DateOfBirth = new DateOnly(1965, 7, 31) },
            new Author { Id = 2, Name = "George R. R. Martin", DateOfBirth = new DateOnly(1948, 9, 20) },
            new Author { Id = 3, Name = "J. R. R. Tolkien", DateOfBirth = new DateOnly(1892, 1, 3) }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Harry Potter and the Philosopher's Stone", PublishedYear = 1997, AuthorId = 1 },
            new Book { Id = 2, Title = "A Game of Thrones", PublishedYear = 1996, AuthorId = 2 },
            new Book { Id = 3, Title = "The Hobbit", PublishedYear = 1937, AuthorId = 3 },
            new Book { Id = 4, Title = "The Lord of the Rings", PublishedYear = 1954, AuthorId = 3 }
        );
    }
}