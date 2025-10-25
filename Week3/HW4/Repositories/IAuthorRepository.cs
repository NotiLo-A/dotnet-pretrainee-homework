using HW4.Models;

namespace HW4.Repositories;

public interface IAuthorRepository
{
    IEnumerable<Author> GetAll();
    Author? GetById(int id);
    Author Add(Author author);
    void Update(Author author);
    void Delete(int id);
    bool Exists(int id);
}