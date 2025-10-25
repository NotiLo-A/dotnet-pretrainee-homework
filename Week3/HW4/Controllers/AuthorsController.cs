using HW4.DTOs;
using HW4.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AuthorResponseDto>> GetAuthors()
    {
        var authors = _authorService.GetAll();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public ActionResult<AuthorResponseDto> GetAuthor(int id)
    {
        var author = _authorService.GetById(id);
        if (author == null) return NotFound(new { message = $"Author with ID {id} not found" });
        return Ok(author);
    }

    [HttpPost]
    public ActionResult<AuthorResponseDto> CreateAuthor(CreateAuthorDto dto)
    {
        var author = _authorService.Create(dto);
        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, UpdateAuthorDto dto)
    {
        try
        {
            _authorService.Update(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        try
        {
            _authorService.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}