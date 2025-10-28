using HW4.Models;

namespace HW4.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(int id);
    Task<Author> AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<(int Id, string Name, int BookCount)>> GetAuthorsWithBookCountsAsync();
    Task<IEnumerable<Author>> FindByNameAsync(string query, bool startsWith);
}