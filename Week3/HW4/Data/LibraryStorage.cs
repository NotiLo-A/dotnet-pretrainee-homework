using HW4.Models;

namespace HW4.Data;

public static class LibraryStorage
{
    private static int _nextAuthorId = 3;
    private static int _nextBookId = 3;
    private static readonly object _authorLock = new();
    private static readonly object _bookLock = new();

    public static List<Author> Authors { get; } = new()
    {
        new Author { Id = 1, Name = "Jane Austen", DateOfBirth = new DateOnly(1775, 12, 16) },
        new Author { Id = 2, Name = "Mark Twain", DateOfBirth = new DateOnly(1835, 11, 30) }
    };

    public static List<Book> Books { get; } = new()
    {
        new Book { Id = 1, Title = "Pride and Prejudice", PublishedYear = 1813, AuthorId = 1 },
        new Book { Id = 2, Title = "Adventures of Huckleberry Finn", PublishedYear = 1884, AuthorId = 2 }
    };

    public static int GetNextAuthorId()
    {
        lock (_authorLock)
        {
            return _nextAuthorId++;
        }
    }

    public static int GetNextBookId()
    {
        lock (_bookLock)
        {
            return _nextBookId++;
        }
    }
}