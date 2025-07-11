using Microsoft.AspNetCore.Mvc;
using test1.Models;
using test1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;     

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext context;  // Dependency injection. It allows us to inject dependencies from the outside, instead of having classes create them internally.
        private readonly ILogger<BookController> _logger; // _ private field parametre olan logger_logger  ın fieldına atanır
        public BookController(AppDbContext context, ILogger<BookController> logger)
        {
            this.context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = context.Books.ToList();
            _logger.LogInformation("Fetchinhg all books from the database");
            return Ok(books);
        }
        [HttpGet]
        [Route("{bookID:int}")]
        public IActionResult GetBookByID(int bookID)
        {
            _logger.LogInformation("Fetching book with ID {BookID} " , bookID);
            var book = context.Books.Find(bookID);
            if (book == null)
            {
                _logger.LogInformation("Book with ID {bookID} not found.", bookID);
                return NotFound("Bokk not found");
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookDto addBookDto)
        {
            _logger.LogInformation("Adding a new book with ID {BookID}" , addBookDto.BookID);
            var bookEntity = new Book()
            {
                BookID = addBookDto.BookID,
                BookName = addBookDto.BookName,
                Author = addBookDto.Author,
                Publisher = addBookDto.Publisher,
                Type = addBookDto.Type,
                Stok = addBookDto.Stok
            };

            context.Books.Add(bookEntity);
            context.SaveChanges();
            _logger.LogInformation("Book with ID {bookEntity.BookID} successfully added.", bookEntity );
            return Ok(bookEntity);
        }
        [HttpPut]
        [Route("{BookID:int}")]
        public IActionResult UpadateBook(int BookID, UpdateBookDto updateBookDto)
        {
            _logger.LogInformation($"Updating book with ID {BookID}.");
            var book = context.Books.Find(BookID);
            if (book == null)
            {
                return NotFound();
            }
            book.BookName = updateBookDto.BookName;
            book.Author = updateBookDto.Author;
            book.Publisher = updateBookDto.Publisher;
            book.Type = updateBookDto.Type;
            book.Stok = updateBookDto.Stok;
            context.SaveChanges();
            _logger.LogInformation($"Book with ID {BookID} successfully updated.");
            return Ok(book + "has been succesfully updated.");
        }
        [HttpDelete]
        [Route("{BookID:int}")]
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
            return Ok("Book has been succesfully removed.");
            
        }

    }
}
