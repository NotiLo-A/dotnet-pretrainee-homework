using HW4.Data;
using HW4.Models;
using Microsoft.EntityFrameworkCore;

namespace HW4.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryContext _context;

    public AuthorRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors.AsNoTracking().ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        return await _context.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Author> AddAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author == null) return;
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Authors.AnyAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<(int Id, string Name, int BookCount)>> GetAuthorsWithBookCountsAsync()
    {
        return await _context.Authors
            .GroupJoin(
                _context.Books,
                a => a.Id,
                b => b.AuthorId,
                (a, books) => new { a.Id, a.Name, BookCount = books.Count() }
            )
            .AsNoTracking()
            .Select(x => new ValueTuple<int, string, int>(x.Id, x.Name, x.BookCount))
            .ToListAsync();
    }

    public async Task<IEnumerable<Author>> FindByNameAsync(string query, bool startsWith)
    {
        var authors = _context.Authors.AsQueryable();
        if (startsWith)
        {
            authors = authors.Where(a => a.Name.StartsWith(query));
        }
        else
        {
            authors = authors.Where(a => a.Name.Contains(query));
        }

        return await authors.AsNoTracking().ToListAsync();
    }
}