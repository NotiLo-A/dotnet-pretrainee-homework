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
    public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAuthors()
    {
        var authors = await _authorService.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("with-counts")]
    public async Task<ActionResult<IEnumerable<AuthorWithBookCountDto>>> GetAuthorsWithCounts()
    {
        var authors = await _authorService.GetAllWithBookCountsAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorResponseDto>> GetAuthor(int id)
    {
        var author = await _authorService.GetByIdAsync(id);
        return Ok(author);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> SearchAuthors([FromQuery] string query, [FromQuery] string mode = "contains")
    {
        var startsWith = string.Equals(mode, "startswith", StringComparison.OrdinalIgnoreCase);
        var authors = await _authorService.SearchByNameAsync(query, startsWith);
        return Ok(authors);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorResponseDto>> CreateAuthor(CreateAuthorDto dto)
    {
        var author = await _authorService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDto dto)
    {
        await _authorService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _authorService.DeleteAsync(id);
        return NoContent();
    }
}