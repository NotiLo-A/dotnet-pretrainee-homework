using Microsoft.AspNetCore.Mvc;
using HW4.Models;
using HW4.Data;

namespace HW4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(LibraryStorage.Books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = LibraryStorage.Books.FirstOrDefault(b => b.Id == id);
            
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found" });
            }

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                return BadRequest(new { message = "Book title is required" });
            }

            if (book.PublishedYear < 1000 || book.PublishedYear > DateTime.Now.Year)
            {
                return BadRequest(new { message = "Invalid publication year" });
            }

            var authorExists = LibraryStorage.Authors.Any(a => a.Id == book.AuthorId);
            if (!authorExists)
            {
                return BadRequest(new { message = $"Author with ID {book.AuthorId} not found" });
            }

            book.Id = LibraryStorage.GetNextBookId();
            LibraryStorage.Books.Add(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var existingBook = LibraryStorage.Books.FirstOrDefault(b => b.Id == id);
            
            if (existingBook == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found" });
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                return BadRequest(new { message = "Book title is required" });
            }

            if (book.PublishedYear < 1000 || book.PublishedYear > DateTime.Now.Year)
            {
                return BadRequest(new { message = "Invalid publication year" });
            }

            var authorExists = LibraryStorage.Authors.Any(a => a.Id == book.AuthorId);
            if (!authorExists)
            {
                return BadRequest(new { message = $"Author with ID {book.AuthorId} not found" });
            }

            existingBook.Title = book.Title;
            existingBook.PublishedYear = book.PublishedYear;
            existingBook.AuthorId = book.AuthorId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = LibraryStorage.Books.FirstOrDefault(b => b.Id == id);
            
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {id} not found" });
            }

            LibraryStorage.Books.Remove(book);

            return NoContent();
        }
    }
}