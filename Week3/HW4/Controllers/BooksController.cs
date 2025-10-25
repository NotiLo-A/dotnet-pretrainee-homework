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
        if (book == null) return NotFound(new { message = $"Book with ID {id} not found" });
        return Ok(book);
    }

    [HttpPost]
    public ActionResult<BookResponseDto> CreateBook(CreateBookDto dto)
    {
        try
        {
            var book = _bookService.Create(dto);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, UpdateBookDto dto)
    {
        try
        {
            _bookService.Update(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            _bookService.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}