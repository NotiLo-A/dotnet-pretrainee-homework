using HW4.Data;
using HW4.Models;
using Microsoft.EntityFrameworkCore;

namespace HW4.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.AsNoTracking().ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null) return;
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByAuthorIdAsync(int authorId)
    {
        return await _context.Books.AnyAsync(b => b.AuthorId == authorId);
    }

    public async Task<IEnumerable<Book>> GetPublishedAfterAsync(int year)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(b => b.PublishedYear > year)
            .ToListAsync();
    }
}