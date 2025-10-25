using HW4.Models;

namespace HW4.Repositories;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    Book Add(Book book);
    void Update(Book book);
    void Delete(int id);
    bool ExistsByAuthorId(int authorId);
}