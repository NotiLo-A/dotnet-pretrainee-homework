using HW4.Data;
using HW4.Models;

namespace HW4.Repositories;

public class BookRepository : IBookRepository
{
    private static readonly object _lock = new();

    public IEnumerable<Book> GetAll()
    {
        lock (_lock)
        {
            return LibraryStorage.Books.ToList();
        }
    }

    public Book? GetById(int id)
    {
        lock (_lock)
        {
            return LibraryStorage.Books.FirstOrDefault(b => b.Id == id);
        }
    }

    public Book Add(Book book)
    {
        lock (_lock)
        {
            book.Id = LibraryStorage.GetNextBookId();
            LibraryStorage.Books.Add(book);
            return book;
        }
    }

    public void Update(Book book)
    {
        lock (_lock)
        {
            var existing = LibraryStorage.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existing != null)
            {
                existing.Title = book.Title;
                existing.PublishedYear = book.PublishedYear;
                existing.AuthorId = book.AuthorId;
            }
        }
    }

    public void Delete(int id)
    {
        lock (_lock)
        {
            var book = LibraryStorage.Books.FirstOrDefault(b => b.Id == id);
            if (book != null) LibraryStorage.Books.Remove(book);
        }
    }

    public bool ExistsByAuthorId(int authorId)
    {
        lock (_lock)
        {
            return LibraryStorage.Books.Any(b => b.AuthorId == authorId);
        }
    }
}