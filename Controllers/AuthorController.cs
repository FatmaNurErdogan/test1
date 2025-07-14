using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test1.Data;
using test1.Models;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext context;

        public AuthorController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            try
            {
                var authors = context.Authors
                    .Where(a => a.Name != null) // Null olan kayıtları filtrele
                    .ToList();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = context.Authors.Find(id);
            if (author == null)
            {
                return NotFound("Author not found");
            }
            return Ok(author);
        }

        [HttpPost]
        public IActionResult AddAuthor(AddAuthor addAuthor)
        {
            if (string.IsNullOrWhiteSpace(addAuthor.Name))
            {
                return BadRequest("Author name cannot be empty.");
            }

            var newAuthor = new Author
            {
                Name = addAuthor.Name,
                Biography = addAuthor.Biography,
                BirthYear = addAuthor.BirthYear
            };

            context.Authors.Add(newAuthor);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.AuthorID }, newAuthor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, UpdateAuthorDto updatedAuthor)
        {
            var existingAuthor = context.Authors.Find(id);
            if (existingAuthor == null)
            {
                return NotFound("Author not found");
            }

            existingAuthor.Name = updatedAuthor.Name ?? existingAuthor.Name;
            existingAuthor.Biography = updatedAuthor.Biography ?? existingAuthor.Biography;
            existingAuthor.BirthYear = updatedAuthor.BirthYear ?? existingAuthor.BirthYear;

            context.SaveChanges();
            return Ok(existingAuthor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = context.Authors.Find(id);
            if (author == null)
            {
                return NotFound("Author not found");
            }

            context.Authors.Remove(author);
            context.SaveChanges();
            return NoContent(); // 204 response
        }
    }
}
