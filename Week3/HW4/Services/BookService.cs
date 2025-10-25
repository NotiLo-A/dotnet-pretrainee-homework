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

    public IEnumerable<BookResponseDto> GetAll()
    {
        return _bookRepository.GetAll()
            .Select(b => new BookResponseDto(b.Id, b.Title, b.PublishedYear, b.AuthorId));
    }

    public BookResponseDto GetById(int id)
    {
        var book = _bookRepository.GetById(id);

        if (book == null) throw new KeyNotFoundException($"Book with ID {id} not found");

        return new BookResponseDto(book.Id, book.Title, book.PublishedYear, book.AuthorId);
    }

    public BookResponseDto Create(CreateBookDto dto)
    {
        if (!_authorRepository.Exists(dto.AuthorId))
            throw new KeyNotFoundException($"Author with ID {dto.AuthorId} not found");

        var book = new Book
        {
            Title = dto.Title,
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId
        };

        var created = _bookRepository.Add(book);
        return new BookResponseDto(created.Id, created.Title, created.PublishedYear, created.AuthorId);
    }

    public void Update(int id, UpdateBookDto dto)
    {
        if (_bookRepository.GetById(id) == null) throw new KeyNotFoundException($"Book with ID {id} not found");

        if (!_authorRepository.Exists(dto.AuthorId))
            throw new KeyNotFoundException($"Author with ID {dto.AuthorId} not found");

        var book = new Book
        {
            Id = id,
            Title = dto.Title,
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId
        };

        _bookRepository.Update(book);
    }

    public void Delete(int id)
    {
        if (_bookRepository.GetById(id) == null) throw new KeyNotFoundException($"Book with ID {id} not found");

        _bookRepository.Delete(id);
    }
}