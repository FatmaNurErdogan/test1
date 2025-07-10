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
        public BorrowedController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetAllBorrowedBooks()
        {
            var borrowedBooks = context.Borrowed.ToList();
            if (borrowedBooks == null || !borrowedBooks.Any())
            {
                return NotFound("No borrowed books found");
            }
            return Ok(borrowedBooks);

        }
        [HttpGet]
        [Route("{borrowedID:int}")]
        public IActionResult GetBorrowedBookByID(int borrowedID)
        {
            var borrowedBook = context.Borrowed.Find(borrowedID);
            if (borrowedBook == null)
            {
                return NotFound("Borrowed book not found");
            }
            return Ok(borrowedBook);
        }
        [HttpPost]
        public IActionResult AddBorrowed(AddBorrowedDto addBorrowedDto)
        {
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
            return Ok(BorrowedEntity);
        }
        [HttpPut]
        [Route("{borrowedID:int}")]
        public IActionResult UpdateBorrowed(int borrowedID, UpdateBorrowedDto updateBorrowedDto)
        {
            var borrowed = context.Borrowed.Find(borrowedID);
            borrowed.BookId = updateBorrowedDto.BookId;
            borrowed.UserId = updateBorrowedDto.UserId;
            borrowed.BorrowDate = updateBorrowedDto.BorrowDate;
            borrowed.ReturnDate = updateBorrowedDto.ReturnDate;
            context.SaveChanges();
            return Ok(borrowed);

        }
        [HttpDelete]
        [Route("{borrowedID:int}")]
        public IActionResult DeleteBorrowed(int borrowedID)
        {
            var borrowed = context.Borrowed.Find(borrowedID);
            if (borrowed == null)
            {
                return NotFound("Borrowed book not found");
            }
            context.Borrowed.Remove(borrowed);
            context.SaveChanges();
            return Ok("Borrowed book has been succesfully deleted.");

        }
    }
}
 