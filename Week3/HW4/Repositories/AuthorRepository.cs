using HW4.Data;
using HW4.Models;

namespace HW4.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private static readonly object _lock = new();

    public IEnumerable<Author> GetAll()
    {
        lock (_lock)
        {
            return LibraryStorage.Authors.ToList();
        }
    }

    public Author? GetById(int id)
    {
        lock (_lock)
        {
            return LibraryStorage.Authors.FirstOrDefault(a => a.Id == id);
        }
    }

    public Author Add(Author author)
    {
        lock (_lock)
        {
            author.Id = LibraryStorage.GetNextAuthorId();
            LibraryStorage.Authors.Add(author);
            return author;
        }
    }

    public void Update(Author author)
    {
        lock (_lock)
        {
            var existing = LibraryStorage.Authors.FirstOrDefault(a => a.Id == author.Id);
            if (existing != null)
            {
                existing.Name = author.Name;
                existing.DateOfBirth = author.DateOfBirth;
            }
        }
    }

    public void Delete(int id)
    {
        lock (_lock)
        {
            var author = LibraryStorage.Authors.FirstOrDefault(a => a.Id == id);
            if (author != null) LibraryStorage.Authors.Remove(author);
        }
    }

    public bool Exists(int id)
    {
        lock (_lock)
        {
            return LibraryStorage.Authors.Any(a => a.Id == id);
        }
    }
}