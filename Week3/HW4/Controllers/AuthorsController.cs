using Microsoft.AspNetCore.Mvc;
using HW4.Models;
using HW4.Data;

namespace HW4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            return Ok(LibraryStorage.Authors);
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = LibraryStorage.Authors.FirstOrDefault(a => a.Id == id);
            
            if (author == null)
            {
                return NotFound(new { message = $"Author with ID {id} not found" });
            }

            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> CreateAuthor(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
            {
                return BadRequest(new { message = "Author name is required" });
            }

            if (author.DateOfBirth > DateTime.Now)
            {
                return BadRequest(new { message = "Date of birth cannot be in the future" });
            }

            author.Id = LibraryStorage.GetNextAuthorId();
            LibraryStorage.Authors.Add(author);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Author author)
        {
            var existingAuthor = LibraryStorage.Authors.FirstOrDefault(a => a.Id == id);
            
            if (existingAuthor == null)
            {
                return NotFound(new { message = $"Author with ID {id} not found" });
            }

            if (string.IsNullOrWhiteSpace(author.Name))
            {
                return BadRequest(new { message = "Author name is required" });
            }

            if (author.DateOfBirth > DateTime.Now)
            {
                return BadRequest(new { message = "Date of birth cannot be in the future" });
            }

            existingAuthor.Name = author.Name;
            existingAuthor.DateOfBirth = author.DateOfBirth;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = LibraryStorage.Authors.FirstOrDefault(a => a.Id == id);
            
            if (author == null)
            {
                return NotFound(new { message = $"Author with ID {id} not found" });
            }

            var hasBooks = LibraryStorage.Books.Any(b => b.AuthorId == id);
            if (hasBooks)
            {
                return BadRequest(new { message = "Cannot delete author who has books" });
            }

            LibraryStorage.Authors.Remove(author);

            return NoContent();
        }
    }
}