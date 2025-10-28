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

    public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        return authors.Select(a => new AuthorResponseDto(a.Id, a.Name, a.DateOfBirth));
    }

    public async Task<AuthorResponseDto?> GetByIdAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);

        if (author == null) throw new KeyNotFoundException($"Author with ID {id} not found");

        return new AuthorResponseDto(author.Id, author.Name, author.DateOfBirth);
    }

    public async Task<AuthorResponseDto> CreateAsync(CreateAuthorDto dto)
    {
        var author = new Author
        {
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth
        };

        var created = await _authorRepository.AddAsync(author);
        return new AuthorResponseDto(created.Id, created.Name, created.DateOfBirth);
    }

    public async Task UpdateAsync(int id, UpdateAuthorDto dto)
    {
        if (!await _authorRepository.ExistsAsync(id)) throw new KeyNotFoundException($"Author with ID {id} not found");

        var author = new Author
        {
            Id = id,
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth
        };

        await _authorRepository.UpdateAsync(author);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _authorRepository.ExistsAsync(id)) throw new KeyNotFoundException($"Author with ID {id} not found");

        if (await _bookRepository.ExistsByAuthorIdAsync(id))
            throw new InvalidOperationException("Cannot delete author who has books");

        await _authorRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AuthorWithBookCountDto>> GetAllWithBookCountsAsync()
    {
        var authorsWithCounts = await _authorRepository.GetAuthorsWithBookCountsAsync();
        return authorsWithCounts.Select(x => new AuthorWithBookCountDto(x.Id, x.Name, x.BookCount));
    }

    public async Task<IEnumerable<AuthorResponseDto>> SearchByNameAsync(string query, bool startsWith)
    {
        var authors = await _authorRepository.FindByNameAsync(query, startsWith);
        return authors.Select(a => new AuthorResponseDto(a.Id, a.Name, a.DateOfBirth));
    }
}