using HW4.DTOs;
using HW4.Models;
using HW4.Repositories;

namespace HW4.Services;

public class BookService : IBookService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return books.Select(b => new BookResponseDto(b.Id, b.Title, b.PublishedYear, b.AuthorId));
    }

    public async Task<BookResponseDto?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null) throw new KeyNotFoundException($"Book with ID {id} not found");

        return new BookResponseDto(book.Id, book.Title, book.PublishedYear, book.AuthorId);
    }

    public async Task<BookResponseDto> CreateAsync(CreateBookDto dto)
    {
        if (!await _authorRepository.ExistsAsync(dto.AuthorId))
            throw new KeyNotFoundException($"Author with ID {dto.AuthorId} not found");

        var book = new Book
        {
            Title = dto.Title,
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId
        };

        var created = await _bookRepository.AddAsync(book);
        return new BookResponseDto(created.Id, created.Title, created.PublishedYear, created.AuthorId);
    }

    public async Task UpdateAsync(int id, UpdateBookDto dto)
    {
        if (await _bookRepository.GetByIdAsync(id) == null) throw new KeyNotFoundException($"Book with ID {id} not found");

        if (!await _authorRepository.ExistsAsync(dto.AuthorId))
            throw new KeyNotFoundException($"Author with ID {dto.AuthorId} not found");

        var book = new Book
        {
            Id = id,
            Title = dto.Title,
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId
        };

        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteAsync(int id)
    {
        if (await _bookRepository.GetByIdAsync(id) == null) throw new KeyNotFoundException($"Book with ID {id} not found");

        await _bookRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BookResponseDto>> GetPublishedAfterAsync(int year)
    {
        var books = await _bookRepository.GetPublishedAfterAsync(year);
        return books.Select(b => new BookResponseDto(b.Id, b.Title, b.PublishedYear, b.AuthorId));
    }
}