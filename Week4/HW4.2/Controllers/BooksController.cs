using HW4.DTOs;
using HW4.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetBooks()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("published-after/{year}")]
    public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetBooksPublishedAfter(int year)
    {
        var books = await _bookService.GetPublishedAfterAsync(year);
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponseDto>> GetBook(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<BookResponseDto>> CreateBook(CreateBookDto dto)
    {
        var book = await _bookService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, UpdateBookDto dto)
    {
        await _bookService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteAsync(id);
        return NoContent();
    }
}