using System.ComponentModel.DataAnnotations.Schema;

namespace test1.Models
{
    public class UpdateBorrowedDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
