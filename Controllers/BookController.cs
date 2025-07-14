using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using test1.Data;
using test1.Models;
using Microsoft.AspNetCore.Authorization;

namespace test1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<BookController> _logger;

        public BookController(AppDbContext context, ILogger<BookController> logger)
        {
            this.context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = context.Books.Include(b => b.Author).ToList();
            _logger.LogInformation("Fetching all books from the database");
            return Ok(books);
        }

        [HttpGet("{bookID:int}")]
        public IActionResult GetBookByID(int bookID)
        {
            _logger.LogInformation("Fetching book with ID {BookID}", bookID);
            var book = context.Books.Include(b => b.Author).FirstOrDefault(b => b.BookID == bookID);

            if (book == null)
            {
                _logger.LogInformation("Book with ID {bookID} not found.", bookID);
                return NotFound("Book not found");
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookDto addBookDto)
        {
            _logger.LogInformation("Adding a new book with ID {BookID}", addBookDto.BookID);
            var bookEntity = new Book()
            {
                BookID = addBookDto.BookID,
                BookName = addBookDto.BookName,
                AuthorID = addBookDto.AuthorID,
                Publisher = addBookDto.Publisher,
                Type = addBookDto.Type,
                Stok = addBookDto.Stok
            };

            context.Books.Add(bookEntity);
            context.SaveChanges();
            _logger.LogInformation("Book with ID {BookID} successfully added.", addBookDto.BookID);
            return Ok(bookEntity);
        }

        [HttpPut("{BookID:int}")]
        public IActionResult UpdateBook(int BookID, UpdateBookDto updateBookDto)
        {
            _logger.LogInformation($"Updating book with ID {BookID}.");
            var book = context.Books.Find(BookID);
            if (book == null)
            {
                return NotFound();
            }

            book.BookName = updateBookDto.BookName;
            book.AuthorID = updateBookDto.AuthorID;
            book.Publisher = updateBookDto.Publisher;
            book.Type = updateBookDto.Type;
            book.Stok = updateBookDto.Stok;

            context.SaveChanges();
            _logger.LogInformation($"Book with ID {BookID} successfully updated.");
            return Ok(book + " has been successfully updated.");
        }

        [HttpDelete("{BookID:int}")]
        public IActionResult DeleteBook(int BookID)
        {
            _logger.LogInformation($"Trying to delete book with ID {BookID}.");
            var book = context.Books.Find(BookID);
            if (book == null)
            {
                _logger.LogInformation($"Book with ID {BookID} not found.");
                return NotFound("Book not found");
            }

            context.Books.Remove(book);
            context.SaveChanges();
            _logger.LogInformation($"Book with ID {BookID} successfully deleted.");
            return Ok("Book has been successfully removed.");
        }
    }
}

