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
    public ActionResult<IEnumerable<BookResponseDto>> GetBooks()
    {
        var books = _bookService.GetAll();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public ActionResult<BookResponseDto> GetBook(int id)
    {
        var book = _bookService.GetById(id);
        return Ok(book);
    }

    [HttpPost]
    public ActionResult<BookResponseDto> CreateBook(CreateBookDto dto)
    {
        var book = _bookService.Create(dto);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, UpdateBookDto dto)
    {
        _bookService.Update(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        _bookService.Delete(id);
        return NoContent();
    }
}