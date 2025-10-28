using HW4.Models;

namespace HW4.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book> AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
    Task<bool> ExistsByAuthorIdAsync(int authorId);
    Task<IEnumerable<Book>> GetPublishedAfterAsync(int year);
}