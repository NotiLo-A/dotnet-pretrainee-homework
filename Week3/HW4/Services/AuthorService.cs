using HW4.DTOs;
using HW4.Models;
using HW4.Repositories;

namespace HW4.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;

    public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
    }

    public IEnumerable<AuthorResponseDto> GetAll()
    {
        return _authorRepository.GetAll()
            .Select(a => new AuthorResponseDto(a.Id, a.Name, a.DateOfBirth));
    }

    public AuthorResponseDto? GetById(int id)
    {
        var author = _authorRepository.GetById(id);
        return author == null
            ? null
            : new AuthorResponseDto(author.Id, author.Name, author.DateOfBirth);
    }

    public AuthorResponseDto Create(CreateAuthorDto dto)
    {
        var author = new Author
        {
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth
        };

        var created = _authorRepository.Add(author);
        return new AuthorResponseDto(created.Id, created.Name, created.DateOfBirth);
    }

    public void Update(int id, UpdateAuthorDto dto)
    {
        if (!_authorRepository.Exists(id)) throw new KeyNotFoundException($"Author with ID {id} not found");

        var author = new Author
        {
            Id = id,
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth
        };

        _authorRepository.Update(author);
    }

    public void Delete(int id)
    {
        if (!_authorRepository.Exists(id)) throw new KeyNotFoundException($"Author with ID {id} not found");

        if (_bookRepository.ExistsByAuthorId(id))
            throw new InvalidOperationException("Cannot delete author who has books");

        _authorRepository.Delete(id);
    }
}