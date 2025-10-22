using HW4.Models;

namespace HW4.Data;

public static class LibraryStorage
{
    private static int _nextAuthorId = 3;
    private static int _nextBookId = 3;

    public static List<Author> Authors { get; set; } = new()
    {
        new Author { Id = 1, Name = "Jane Austen", DateOfBirth = new DateTime(1775, 12, 16) },
        new Author { Id = 2, Name = "Mark Twain", DateOfBirth = new DateTime(1835, 11, 30) }
    };

    public static List<Book> Books { get; set; } = new()
    {
        new Book { Id = 1, Title = "Pride and Prejudice", PublishedYear = 1813, AuthorId = 3 },
        new Book { Id = 2, Title = "Adventures of Huckleberry Finn", PublishedYear = 1884, AuthorId = 4 }
    };

    public static int GetNextAuthorId()
    {
        return _nextAuthorId++;
    }

    public static int GetNextBookId()
    {
        return _nextBookId++;
    }
}