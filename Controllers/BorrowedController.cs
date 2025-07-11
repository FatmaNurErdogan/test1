using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test1.Data;
using test1.Models;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<BorrowedController> _logger;
        public BorrowedController(AppDbContext context, ILogger<BorrowedController>logger)
        {
            this.context = context;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllBorrowedBooks()
        {
            _logger.LogInformation("Fetching all borrowed books from the database");
            var borrowedBooks = context.Borrowed.ToList();
            if (borrowedBooks == null || !borrowedBooks.Any())
            {
                return NotFound("No borrowed books found");
            }
            _logger.LogInformation("Total borrowed books found: {Count}", borrowedBooks.Count);
            return Ok(borrowedBooks);

        }
        [HttpGet]
        [Route("{borrowedID:int}")]
        public IActionResult GetBorrowedBookByID(int borrowedID)
        {
            _logger.LogInformation("Fetching borrowed book with ID {BorrowedID}", borrowedID);
            var borrowedBook = context.Borrowed.Find(borrowedID);
            if (borrowedBook == null)
            {
                return NotFound("Borrowed book not found");
            }
            _logger.LogInformation("Borrowed book with ID {BorrowedID} found", borrowedID);
            return Ok(borrowedBook);
        }
        [HttpPost]
        public IActionResult AddBorrowed(AddBorrowedDto addBorrowedDto)
        {
            _logger.LogInformation("Adding a new borrowed book with ID {BorrowedID}", addBorrowedDto.BorrowedID);
            var BorrowedEntity = new Borrowed()
            {
                BorrowedID = addBorrowedDto.BorrowedID,
                BookId = addBorrowedDto.BookId,
                UserId = addBorrowedDto.UserId,
                BorrowDate = addBorrowedDto.BorrowDate,
                ReturnDate = addBorrowedDto.ReturnDate
            };
            context.Borrowed.Add(BorrowedEntity);
            context.SaveChanges();
            _logger.LogInformation("Borrowed book with ID {BorrowedID} successfully added", addBorrowedDto.BorrowedID);
            return Ok(BorrowedEntity);
        }
        [HttpPut]
        [Route("{borrowedID:int}")]
        public IActionResult UpdateBorrowed(int borrowedID, UpdateBorrowedDto updateBorrowedDto)
        {
            _logger.LogInformation("Updating borrowed book with ID {BorrowedID}", borrowedID);
            var borrowed = context.Borrowed.Find(borrowedID);
            borrowed.BookId = updateBorrowedDto.BookId;
            borrowed.UserId = updateBorrowedDto.UserId;
            borrowed.BorrowDate = updateBorrowedDto.BorrowDate;
            borrowed.ReturnDate = updateBorrowedDto.ReturnDate;
            context.SaveChanges();
            _logger.LogInformation("Borrowed book with ID {BorrowedID} has been successfully updated", borrowedID);
            return Ok(borrowed);

        }
        [HttpDelete]
        [Route("{borrowedID:int}")]
        public IActionResult DeleteBorrowed(int borrowedID)
        {
            _logger.LogInformation("Deleting borrowed book with ID {BorrowedID}", borrowedID);
            var borrowed = context.Borrowed.Find(borrowedID);
            if (borrowed == null)
            {
                return NotFound("Borrowed book not found");
            }
            context.Borrowed.Remove(borrowed);
            context.SaveChanges();
            _logger.LogInformation("Borrowed book with ID {BorrowedID} has been successfully deleted", borrowedID);
            return Ok("Borrowed book has been succesfully deleted.");

        }
    }
}
 